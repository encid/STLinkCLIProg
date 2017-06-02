using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string STPath = @"C:\Program Files (x86)\STMicroelectronics\STM32 ST-LINK Utility\ST-LINK Utility\ST-LINK_CLI.exe";
            string firmwareLocation = @"H:\Fetco\cbs2030_3015_w_bl.hex";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = STPath;
            start.Arguments = "-p " + firmwareLocation + " -v";
            start.UseShellExecute = false;
            start.CreateNoWindow = false;

            Process p = new Process();
            p.StartInfo = start;

            p.Start();
            p.WaitForExit();
            var exitCode = p.ExitCode;

            if (exitCode == 0)
            {
                label1.Text = "Programmed OK!";
            }
            else
                label1.Text = "ERROR Programming!";

            p.Close();
        }
    }
}
