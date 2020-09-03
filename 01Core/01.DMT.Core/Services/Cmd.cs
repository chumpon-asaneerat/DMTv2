using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DMT.Services
{
    public class CommandLine
    {
        public string FileName { get; set;  }

        public bool UseShellExecute { get; set; }
        public bool RedirectStandardOutput { get; set; }
        public bool CreateNoWindow { get; set; }

        public CommandLine()
        {
            FileName = "netsh.exe";
            UseShellExecute = false;
            RedirectStandardOutput = true;
            CreateNoWindow = true;

        }

        public void Run(string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = FileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = UseShellExecute;
            process.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
            process.StartInfo.CreateNoWindow = CreateNoWindow;
            process.Start();
        }
    }
}
