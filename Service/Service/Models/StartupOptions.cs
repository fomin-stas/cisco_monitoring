using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace Service.Models
{
    public class StartupOptions
    {
        [Option('d', "database", Required = false, HelpText = "Database file path.")]
        public string DatabaseFilePath { get; set; }

        [Option('l', "log", Required = false, HelpText = "Log file path.")]
        public string LogFilePath { get; set; }
    }
}
