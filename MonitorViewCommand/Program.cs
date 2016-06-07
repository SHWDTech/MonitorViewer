using System;

namespace MonitorViewCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            MonitorViewer.MonitorViewer viewer = new MonitorViewer.MonitorViewer();
            viewer.SetConnectServer("localhost:6330");
            viewer.SetupCamera("588101720");
            Console.WriteLine(viewer.StartMonitor());
            Console.ReadKey();
        }
    }
}
