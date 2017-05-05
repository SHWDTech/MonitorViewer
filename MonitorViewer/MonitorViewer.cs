using System;
using System.IO;
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

        /// <summary>
        /// 获取摄像头列表
        /// </summary>
        public ArrayObject CameraList => GlobalObject.Array.ConstructArray(HikAction.GetCameraList());

        /// <summary>
        /// 获取摄像头ID列表
        /// </summary>
        public ArrayObject CameraIdList => GlobalObject.Array.ConstructArray(HikAction.GetCameraIdList());

        /// <summary>
        /// 图片储存目录
        /// </summary>
        private const string PicUrl = "c:\\CameraPicture";

        public string CurrentServer => ServerConnecter.Server;

        public int Speed
        {
            get => HikAction.Speed;
            set => HikAction.Speed = value;
        }

        /// <summary>
        /// 摄像头ID
        /// </summary>
        private string _cameraId;

        /// <summary>
        /// 摄像头产品ID
        /// </summary>
        public static string CameraProductId { get; set; }

        /// <summary>
        /// 是否已完成初始化设置
        /// </summary>
        private bool _setuped;

        /// <summary>
        /// 云台控制命令
        /// </summary>
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

        /// <summary>
        /// 设置连接服务器
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public int SetConnectServer(string server)
        {
            try
            {
                ServerConnecter.Server = server;
                var paramDictionary = ServerConnecter.GetCameraInfomation();
                HikAction.SetUpParams(paramDictionary);
                var result = HikAction.InitLib();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1005;
            }
        }

        /// <summary>
        /// 初始化摄像头连接参数
        /// </summary>
        /// <param name="cameraProdectId"></param>
        /// <returns></returns>
        public string SetupCamera(string cameraProdectId)
        {
            try
            {
                CameraProductId = cameraProdectId;
                var safeKey = ServerConnecter.GetSafeKey(CameraProductId);
                safeKey = safeKey.Replace("\"", string.Empty);
                if (!string.IsNullOrWhiteSpace(safeKey))
                {
                    HikAction.SafeKey = safeKey;
                    _setuped = true;
                    var camera = HikAction.CameraList.FirstOrDefault(obj => obj.ToString().Contains(CameraProductId));
                    if (camera != null)
                    {
                        var index = HikAction.CameraList.IndexOf(camera);
                        if (index < 0) return "-1006";
                        _cameraId = HikAction.CameraIdList[index];
                    }
                    else
                    {
                        return "-1003";
                    }

                    return "0";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + $"Server:{ServerConnecter.Server}";
            }

            return "-1002";
        }

        /// <summary>
        /// 开始预览
        /// </summary>
        /// <returns></returns>
        public int StartMonitor()
        {
            if (!_setuped) return -99;
            return HikAction.StartPlay(ViewerBox.Handle, _cameraId, HikAction.SafeKey);
        }

        /// <summary>
        /// 停止预览
        /// </summary>
        /// <returns></returns>
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
            var success = Enum.TryParse(dir, false, out PTZCommand cmd);

            if (!success)
            {
                cmd = PTZCommand.UNKNOW;
            }

            return cmd;
        }

        /// <summary>
        /// 拍摄照片
        /// </summary>
        /// <returns></returns>
        public bool CapturePicture()
        {
            var fileName = $"{PicUrl}\\{DateTime.Now:yyyyMMddhhmmssfff}.jpg";
            if (!Directory.Exists(PicUrl))
            {
                try
                {
                    Directory.CreateDirectory(PicUrl);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            var picResult = HikAction.CapturePicture(fileName);
            if (!picResult) return false;

            if (!File.Exists(fileName)) return false;

            var fileBytes = File.ReadAllBytes(fileName);

            picResult = ServerConnecter.PostCapturePicture(fileBytes);

            File.Delete(fileName);

            return picResult;
        }

        public new void Dispose()
        {
            HkSdk.OpenSDK_FiniLib();
            base.Dispose();
        }
    }
}
