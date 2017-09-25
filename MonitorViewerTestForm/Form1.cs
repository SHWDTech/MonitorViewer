using System;
using System.Diagnostics;
using System.Windows.Forms;
using MonitorViewer;

namespace MonitorViewerTestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            monitorViewer1.SetConnectServer("yun.shweidong.com:10888");
            monitorViewer1.SetupCamera("716109446");

            var start = $"{DateTime.Now:yyyy-MM-dd} 00:00:00";
            var end = $"{DateTime.Now:yyyy-MM-dd} 23:59:59";
            var ret = monitorViewer1.StartSearch(start, end);
            Debug.WriteLine(ret);
            Debug.WriteLine(monitorViewer1.GetLastErrorCode());
            while (HikAction.PlayBackSearchResults == null)
            {
            }
            ret = monitorViewer1.StartPlayBack(HikAction.PlayBackSearchResults.FileList[0].StartTime, HikAction.PlayBackSearchResults.FileList[0].EndTime);
            Debug.WriteLine(ret);
            Debug.WriteLine(monitorViewer1.GetLastErrorCode());
        }
    }
}
