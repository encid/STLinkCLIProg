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

        private void button2_Click(object sender, EventArgs e)
        {
            string STPath = @"C:\Program Files (x86)\STMicroelectronics\STM32 ST-LINK Utility\ST-LINK Utility\ST-LINK_CLI.exe";
            string firmwareLocation = @"H:\Fetco\cbs2030_3015_w_bl.hex";

            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.EnableRaisingEvents = false;
                proc.StartInfo.RedirectStandardOutput = true;
                //proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.Verb = "open";
                proc.StartInfo.FileName = STPath;
                proc.StartInfo.Arguments = "-p " + firmwareLocation + " -v";
                proc.Start();
                String sLine = "";
                while ((sLine = proc.StandardOutput.ReadLine()) != null)
                {
                    //System.Console.WriteLine(sLine);
                    tb.AppendText(sLine);
                }
                proc.WaitForExit(); //Jon Skeet was here!
                var errorCode = proc.ExitCode;
                proc.Close();
            }

        }


        void runCommand()
        {
            string STPath = @"C:\Program Files (x86)\STMicroelectronics\STM32 ST-LINK Utility\ST-LINK Utility\ST-LINK_CLI.exe";
            string firmwareLocation = @"H:\Fetco\cbs2030_3015_w_bl.hex";

            //* Create your Process
            Process process = new Process();
            process.StartInfo.FileName = STPath;
            process.StartInfo.Arguments = "-p " + firmwareLocation + " -v";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += (s, e) => tb.AppendText("\n" + e.Data);
            process.ErrorDataReceived += (s, e) => tb.AppendText("\n" + e.Data);
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            runCommand();
        }
    }
}
