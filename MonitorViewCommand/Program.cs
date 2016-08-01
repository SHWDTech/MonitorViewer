using System;

namespace MonitorViewCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            MonitorViewer.MonitorViewer viewer = new MonitorViewer.MonitorViewer();
            Console.WriteLine(viewer.SetConnectServer("localhost:6330"));
            Console.WriteLine(viewer.SetupCamera("626519901"));
            Console.WriteLine(viewer.StartMonitor());
            Console.WriteLine(viewer.CapturePicture());
            Console.ReadKey();
        }
    }
}
