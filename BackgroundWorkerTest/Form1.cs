using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BackgroundWorkerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += (o, ev) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(50);
                    bw.ReportProgress(i);
                }
            };

            bw.ProgressChanged += (o, ev) => 
            {
                this.progressBar1.Value = ev.ProgressPercentage;
            };

            bw.RunWorkerCompleted += (o, ev) =>
            {
                this.textBox1.Text = "Complete";
                
            };
            bw.RunWorkerAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String hostname = Dns.GetHostName();
            foreach (var ip in Dns.GetHostAddresses(hostname))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    MessageBox.Show(ip.ToString());
            }
        }
    }
}
