using System;
using MonitorViewer;

namespace MonitorViewCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            MonitorViewer.MonitorViewer viewer = new MonitorViewer.MonitorViewer();
            Console.WriteLine(viewer.SetConnectServer("yun.shweidong.com:10888"));
            Console.WriteLine(viewer.SetupCamera("717276389"));
            var ret = viewer.StartMonitor();
            if (ret != 0)
            {
                Console.WriteLine($"ErrorCode:{HkSdk.OpenSDK_GetLastErrorCode()}");
                Console.WriteLine($"ErrorDesc:{HkSdk.OpenSDK_GetLastErrorDesc()}");
            }
            else
            {
                Console.WriteLine("启动成功。");
            }

            //var suc = viewer.CapturePicture();
            //Console.WriteLine(suc);
            viewer.ControlPlatform("ZOOMOUT");

            Console.ReadKey();
        }
    }
}
