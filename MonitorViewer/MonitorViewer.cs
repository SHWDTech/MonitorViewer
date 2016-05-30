using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.JScript;
using TestActiveXControl;

namespace MonitorViewer
{
    [Guid("41353907-A6CF-4BDF-9AE0-EB06F8C48414"), ComVisible(true)]

    public partial class MonitorViewer : UserControl, IObjectSafety
    {
        public MonitorViewer()
        {
            InitializeComponent();
            HkAction.InitLib();
        }

        public ArrayObject CameraList => GlobalObject.Array.ConstructArray(HkAction.GetCameraList());

        public ArrayObject CameraIdList => GlobalObject.Array.ConstructArray(HkAction.GetCameraIdList());

        public int Speed
        {
            get { return HikCameraControl.Speed; }
            set { HikCameraControl.Speed = value; }
        }

        public int ChannelNumber
        {
            get { return HikCameraControl.ChannelNumber; }
            set { HikCameraControl.ChannelNumber = value; }
        }

        private string _cameraId;

        private string _safeKey;

        private bool _setuped;

        private PTZCommand _ptzCommand;

        #region IObjectSafety 成员
        private const string IidIDispatch = "{00020400-0000-0000-C000-000000000046}";
        private const string IidIDispatchEx = "{a6ef9860-c720-11d0-9337-00a0c90dcaa9}";
        private const string IidIPersistStorage = "{0000010A-0000-0000-C000-000000000046}";
        private const string IidIPersistStream = "{00000109-0000-0000-C000-000000000046}";
        private const string IidIPersistPropertyBag = "{37D84F60-42CB-11CE-8135-00AA004BB851}";

        private const int InterfacesafeForUntrustedCaller = 0x00000001;
        private const int InterfacesafeForUntrustedData = 0x00000002;
        private const int SOk = 0;
        private const int EFail = unchecked((int)0x80004005);
        private const int ENointerface = unchecked((int)0x80004002);

        private bool _fSafeForScripting = true;
        private bool _fSafeForInitializing = true;

        // ReSharper disable once RedundantAssignment
        public int GetInterfaceSafetyOptions(ref Guid riid, ref int pdwSupportedOptions, ref int pdwEnabledOptions)
        {
            int rslt;

            var strGuid = riid.ToString("B");
            pdwSupportedOptions = InterfacesafeForUntrustedCaller | InterfacesafeForUntrustedData;
            switch (strGuid)
            {
                case IidIDispatch:
                case IidIDispatchEx:
                    rslt = SOk;
                    pdwEnabledOptions = 0;
                    if (_fSafeForScripting)
                        pdwEnabledOptions = InterfacesafeForUntrustedCaller;
                    break;
                case IidIPersistStorage:
                case IidIPersistStream:
                case IidIPersistPropertyBag:
                    rslt = SOk;
                    pdwEnabledOptions = 0;
                    if (_fSafeForInitializing)
                        pdwEnabledOptions = InterfacesafeForUntrustedData;
                    break;
                default:
                    rslt = ENointerface;
                    break;
            }

            return rslt;
        }

        public int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
        {
            var rslt = EFail;
            var strGuid = riid.ToString("B");
            switch (strGuid)
            {
                case IidIDispatch:
                case IidIDispatchEx:
                    if (((dwEnabledOptions & dwOptionSetMask) == InterfacesafeForUntrustedCaller) && _fSafeForScripting)
                        rslt = SOk;
                    break;
                case IidIPersistStorage:
                case IidIPersistStream:
                case IidIPersistPropertyBag:
                    if (((dwEnabledOptions & dwOptionSetMask) == InterfacesafeForUntrustedData) && _fSafeForInitializing)
                        rslt = SOk;
                    break;
                default:
                    rslt = ENointerface;
                    break;
            }

            return rslt;
        }

        #endregion

        public void SetupCamera(string cameraId, string safeKey, string deviceSerial)
        {
            _cameraId = cameraId;
            _safeKey = safeKey;
            HikCameraControl.DeviceSerial = deviceSerial;

            _setuped = true;
        }

        public int StartMonitor()
        {
            if (!_setuped) return -99;
            return HkAction.StartPlay(ViewerBox.Handle, _cameraId, _safeKey);
        }

        public int StopMonitor()
        {
            if (!string.IsNullOrWhiteSpace(_cameraId))
            {
                return HkAction.StopPlay();
            }

            return -100;
        }

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public int ControlPlatform(string dir)
        {
            _ptzCommand = GetPtzCommand(dir);
            if (_ptzCommand == PTZCommand.UNKNOW)
            {
                return -98;
            }

            return HkAction.PtzCtrl(_cameraId, _ptzCommand, PTZACtion.START, Speed);
        }

        /// <summary>
        /// 停止云台控制
        /// </summary>
        /// <returns></returns>
        public int StopControlPlatform()
            => HkAction.PtzCtrl(_cameraId, _ptzCommand, PTZACtion.STOP, Speed);

        /// <summary>
        /// 将字符串转换为云台指令
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private PTZCommand GetPtzCommand(string dir)
        {
            PTZCommand cmd;
            var success = Enum.TryParse(dir, false, out cmd);

            if (!success)
            {
                cmd = PTZCommand.UNKNOW;
            }

            return cmd;
        }
    }
}
