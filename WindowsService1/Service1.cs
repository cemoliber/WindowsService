using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {

        System.Timers.Timer timer = new System.Timers.Timer();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteFile("Service Started Running "+DateTime.Now);
            timer.Interval = 5000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteFile("Service Stopped" + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteFile("Service Continues to Run");
        }

        public void WriteFile(string message)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Logs";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string txtPath = AppDomain.CurrentDomain.BaseDirectory + "/Logs/service.txt";
            if (!File.Exists(txtPath))
            {
                using (StreamWriter sw = File.CreateText(txtPath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(txtPath))
                {
                    sw.WriteLine(message);
                }
            }


        }

    }
}
