using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Service.Models
{
    public static class CommandLineHelper
    {
        public static string GetConfigFilePath(string[] args)
        {
            var startupOptions = new StartupOptions();

            if (CommandLine.Parser.Default.ParseArguments(args, startupOptions))
            {
                if (!string.IsNullOrEmpty(startupOptions.DatabaseFilePath) && File.Exists(startupOptions.DatabaseFilePath))
                    return startupOptions.DatabaseFilePath;
            }

            return null;
        }
    }
}
