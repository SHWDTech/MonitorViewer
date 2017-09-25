using System;
using MonitorViewer;

namespace MonitorViewCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            var viewer = new MonitorViewer.MonitorViewer();
            Console.WriteLine(viewer.SetConnectServer("yun.shweidong.com:10888"));
            Console.WriteLine(viewer.SetupCamera("716109446"));
            var ret = viewer.StartMonitor();
            if (ret != 0)
            {
                Console.WriteLine($@"ErrorCode:{HkSdk.OpenSDK_GetLastErrorCode()}");
                Console.WriteLine($@"ErrorDesc:{HkSdk.OpenSDK_GetLastErrorDesc()}");
            }
            else
            {
                Console.WriteLine(@"启动成功。");
            }

            Console.ReadKey();
        }
    }
}
