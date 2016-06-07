using System;
using System.Linq;
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
        }

        public ArrayObject CameraList => GlobalObject.Array.ConstructArray(HikAction.GetCameraList());

        public ArrayObject CameraIdList => GlobalObject.Array.ConstructArray(HikAction.GetCameraIdList());

        public int Speed
        {
            get { return HikAction.Speed; }
            set { HikAction.Speed = value; }
        }

        private string _cameraId;

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

        public int SetConnectServer(string server)
        {
            try
            {
                var paramDictionary = ServerConnecter.GetCameraInfomation(server);
                HikAction.SetUpParams(paramDictionary);
                var result = HikAction.InitLib();
                return result;
            }
            catch (Exception)
            {
                return -1005;
            }
        }

        public int SetupCamera(string cameraId)
        {
            try
            {
                var safeKey = ServerConnecter.GetSafeKey(cameraId);
                safeKey = safeKey.Replace("\"", string.Empty);
                if (!string.IsNullOrWhiteSpace(safeKey))
                {
                    HikAction.SafeKey = safeKey;
                    _setuped = true;
                    var camera = HikAction.CameraList.FirstOrDefault(obj => obj.ToString().Contains(cameraId));
                    if (camera != null)
                    {
                        var index = HikAction.CameraList.IndexOf(camera);
                        if (index < 0) return -1006;
                        _cameraId = HikAction.CameraIdList[index];
                    }
                    else
                    {
                        return -1003;
                    }

                    return 0;
                }
            }
            catch (Exception)
            {
                return -1004;
            }
            
            return -1002;
        }

        public int StartMonitor()
        {
            if (!_setuped) return -99;
            return HikAction.StartPlay(ViewerBox.Handle, _cameraId, HikAction.SafeKey);
        }

        public int StopMonitor()
        {
            if (!string.IsNullOrWhiteSpace(_cameraId))
            {
                return HikAction.StopPlay();
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

            return HikAction.PtzCtrl(_cameraId, _ptzCommand, PTZACtion.START, Speed);
        }

        /// <summary>
        /// 停止云台控制
        /// </summary>
        /// <returns></returns>
        public int StopControlPlatform()
            => HikAction.PtzCtrl(_cameraId, _ptzCommand, PTZACtion.STOP, Speed);

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

        public bool CapturePicture()
            => HikAction.CapturePicture("");
    }
}
