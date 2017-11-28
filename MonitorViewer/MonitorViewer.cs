using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.JScript;
using Newtonsoft.Json;
using TestActiveXControl;

namespace MonitorViewer
{
    [Guid("41353907-A6CF-4BDF-9AE0-EB06F8C48414"), ComVisible(true)]
    public partial class MonitorViewer : UserControl, IObjectSafety
    {
        public MonitorViewer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            cmbFiles.DisplayMember = "Text";
            ViewerBox.Paint += WdCameraViewerPaint;
            HikAction.OnSearchPlaybackGetResult += OnGetSearchResult;
            _stopTimer = new System.Timers.Timer
            {
                Interval = 600000,
                Enabled = true
            };
            _stopTimer.Elapsed += (obj, args) =>
            {
                StopMonitor(null, null);
            };
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
        /// 回调信息查询结果
        /// </summary>
        public string PlayBackSearchResult => JsonConvert.SerializeObject(HikAction.PlayBackSearchResults);

        /// <summary>
        /// 图片储存目录
        /// </summary>
        private const string PicUrl = "c:\\CameraPicture";

        public string CurrentServer => ServerConnecter.Server;

        private bool _isPlaybacking;

        private bool _isExisting;

        private string _executingException = string.Empty;

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

        public static int DevId { get; set; } = -1;

        private string _displayMessage = string.Empty;

        private readonly StringFormat _displayStringFormat = new StringFormat
        {
            LineAlignment = StringAlignment.Far,
            Alignment = StringAlignment.Near
        };

        private readonly Brush _displayBrush = Brushes.White;

        private bool _isPrewing;

        private System.Timers.Timer _stopTimer;

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
                if (!string.IsNullOrEmpty(safeKey))
                {
                    HikAction.SafeKey = safeKey;
                    _setuped = true;
                    var camera = HikAction.CameraList.Find(obj => obj.ToString().Contains(CameraProductId));
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

        public string SetupDevId(string devId)
        {
            if (string.IsNullOrEmpty(devId)) return "-1";
            DevId = int.Parse(devId);

            return "0";
        }

        /// <summary>
        /// PICTUREBOX控件响应PAINT事件显示问题提示。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WdCameraViewerPaint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(_displayMessage)) return;
            using (var myFont = new Font("simsun", 20))
            {
                e.Graphics.DrawString(_displayMessage, myFont, _displayBrush, ClientRectangle, _displayStringFormat);
            }
        }

        /// <summary>
        /// 开始预览
        /// </summary>
        /// <returns></returns>
        public int StartMonitor()
        {
            if (!_setuped) return -99;
            var ret = HikAction.StartPlay(ViewerBox.Handle, _cameraId, HikAction.SafeKey);
            if (ret == 0)
            {
                _isPrewing = true;
                StartDisplay();
                _stopTimer.Start();
            }
            return ret;
        }

        private void StartMonitor(object sender, EventArgs e)
        {
            if (_isPlaybacking) return;
            var ret = StartMonitor();
            if (ret == 0)
            {
                StopControlPlatform();
                SetContrilStatus(true);
            }
        }

        private void SetContrilStatus(bool status)
        {
            btnStopRealPlay.Enabled = btnPtzUp.Enabled =
                btnPtzDwon.Enabled = btnPtzLeft.Enabled =
                btnPtzZoomin.Enabled = btnPtzZoomout.Enabled =
                    btnPtzRight.Enabled = btnTakePicture.Enabled = status;
            btnPlayBack.Enabled = btnStartRealPlay.Enabled = !status;
        }

        public int StartPlayBack(string startTime, string endTime)
        {
            var ret = HikAction.StartPlayBack(ViewerBox.Handle, _cameraId, HikAction.SafeKey,
                startTime, endTime);
            return ret;
        }

        public int StopPlayBack()
            => HikAction.StopPlayBack();

        private void StartDisplay()
        {
            var thread = new Thread(() =>
            {
                while (!_isExisting)
                {
                    if (_isPrewing || DevId == -1)
                    {
                        try
                        {
                            var dataXmlStr = ServerConnecter.FetchData(DevId);
                            if (!string.IsNullOrEmpty(dataXmlStr))
                            {
                                var data = XmlSerializerHelper.DeSerialize<DeviceRecentData>(dataXmlStr);
                                _displayMessage =
                                    $"{"PM10".PadRight(4)}:{data.Tp} ug/m³\r\n";
                                ViewerBox.Invalidate();
                            }
                        }
                        catch (Exception ex)
                        {
                            _executingException = ex.Message;
                        }
                    }
                    Thread.Sleep(10000);
                }
            })
            {
                IsBackground = true
            };

            thread.Start();
        }

        /// <summary>
        /// 停止预览
        /// </summary>
        /// <returns></returns>
        public int StopMonitor()
        {
            if (!string.IsNullOrEmpty(_cameraId))
            {
                var ret = HikAction.StopPlay();
                if (ret == 0)
                {
                    _isPrewing = false;
                    _displayMessage = string.Empty;
                    ViewerBox.Invalidate();
                    _stopTimer.Stop();
                }
                return ret;
            }

            return -100;
        }

        private void StopMonitor(object sender, EventArgs e)
        {
            var ret = StopMonitor();
            if (ret == 0)
            {
                SetContrilStatus(false);
            }
        }

        private void PtzUp(object sender, MouseEventArgs e)
        {
            ControlPlatform("UP");
        }

        private void PtzDown(object sender, MouseEventArgs e)
        {
            ControlPlatform("DOWN");
        }

        private void PtzLeft(object sender, MouseEventArgs e)
        {
            ControlPlatform("LEFT");
        }

        private void PtzRight(object sender, MouseEventArgs e)
        {
            ControlPlatform("RIGHT");
        }

        private void PtzZoomout(object sender, MouseEventArgs e)
        {
            ControlPlatform("ZOOMOUT");
        }

        private void PtzZoomin(object sender, MouseEventArgs e)
        {
            ControlPlatform("ZOOMIN");
        }

        private void PtzStop(object sender, MouseEventArgs e)
        {
            StopControlPlatform();
        }

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public int ControlPlatform(string dir)
        {
            _stopTimer.Stop();
            _stopTimer.Start();
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
            var cmd = (PTZCommand)Enum.Parse(typeof(PTZCommand), dir);

            return cmd;
        }

        public int GetLastErrorCode()
            => HikAction.GetLastErrorCode();

        public int StartSearch(string startTime, string endTime)
            => HikAction.StartSearch(_cameraId, startTime, endTime);

        public string GetExectingException => _executingException;

        private void SearchFiles(object sender, EventArgs e)
        {
            var start = $"{dtpStart.Value:yyyy-MM-dd} 00:00:00";
            var end = $"{dtpEnd.Value:yyyy-MM-dd} 23:59:59";
            StartSearch(start, end);
            cmbFiles.Items.Clear();
        }

        private void OnGetSearchResult(PlayBackSearchResult args)
        {
            if (args?.FileList != null && args.FileList.Length > 0)
            {
                for (var i = 0; i < args.FileSize; i++)
                {
                    var file = args.FileList[i];
                    cmbFiles.Items.Add(new SearchItem($"{file.StartTime} - {file.EndTime}", file));
                }
                cmbFiles.SelectedIndex = 0;
                if (!_isPrewing)
                {
                    btnPlayBack.Enabled = true;
                }
            }
        }

        private void PlayBackControl(object sender, EventArgs e)
        {
            if (_isPrewing || !(cmbFiles.SelectedItem is SearchItem)) return;
            if (!_isPlaybacking)
            {
                var item = ((SearchItem)cmbFiles.SelectedItem).Info;
                var ret = StartPlayBack(item.StartTime, item.EndTime);
                _isPlaybacking = ret == 0;
                btnStartRealPlay.Enabled = !_isPlaybacking;
            }
            else
            {
                var ret = StopPlayBack();
                _isPlaybacking = ret != 0;
                btnStartRealPlay.Enabled = !_isPlaybacking;
                Thread.Sleep(100);
                ViewerBox.Invalidate();
            }

            btnPlayBack.Text = _isPlaybacking ? "停止回放" : "开始回放";
        }

        private void TakePicture(object sender, EventArgs e)
        {
            if (!_isPrewing) return;
            MessageBox.Show(CapturePicture() ? @"拍照完成" : @"拍照失败", @"提示", MessageBoxButtons.OK);
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

        public string WindDirectionString(double dir)
        {
            if (dir >= 348.76 || dir <= 11.25) return "北";
            if (dir >= 11.26 && dir <= 33.75) return "北东北";
            if (dir >= 33.76 && dir <= 56.25) return "东北";
            if (dir >= 56.26 && dir <= 78.75) return "东东北";
            if (dir >= 78.76 && dir <= 101.25) return "东";
            if (dir >= 101.26 && dir <= 123.75) return "东东南";
            if (dir >= 123.76 && dir <= 146.25) return "东南";
            if (dir >= 146.26 && dir <= 168.75) return "南东南";
            if (dir >= 168.76 && dir <= 191.25) return "南";
            if (dir >= 191.26 && dir <= 213.75) return "南西南";
            if (dir >= 213.76 && dir <= 236.25) return "西南";
            if (dir >= 236.26 && dir <= 258.75) return "西西南";
            if (dir >= 258.76 && dir <= 281.25) return "西";
            if (dir >= 281.26 && dir <= 303.75) return "西西北";
            if (dir >= 303.76 && dir <= 326.25) return "西北";
            if (dir >= 326.26 && dir <= 348.75) return "北西北";

            return "未知";
        }

        public new void Dispose()
        {
            _isExisting = true;
            StopControlPlatform();
            HkSdk.OpenSDK_FiniLib();
            base.Dispose();
        }
    }

    class SearchItem
    {
        public SearchItem(string text, PlaybackFileInfo info)
        {
            Text = text;
            Info = info;
        }

        public string Text { get; set; }

        public PlaybackFileInfo Info { get; set; }
    }
}
