using System.Windows.Forms;

namespace MonitorViewerTestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            monitorViewer1.SetConnectServer("yun.shweidong.com:10888");
            monitorViewer1.SetupDevId("1");
        }
    }
}
