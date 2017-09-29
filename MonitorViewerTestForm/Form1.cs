using System;
using System.Windows.Forms;

namespace MonitorViewerTestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            monitorViewer1.SetConnectServer("localhost:6330");
            monitorViewer1.SetupCamera("716109446");
            //monitorViewer1.SetupDevId("115");
        }
    }
}
