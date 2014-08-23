using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using StaticValuesDll;

namespace Service.Models
{
    public class ClientLogService
    {
        public static string DefaultLogFilePath = Path.GetPathRoot(Environment.SystemDirectory) + "WaterGateService\\ClientLog.log";
        private static string _logFilePath;

        private readonly object _lockObject = new object();

        public static void Initialize(string path)
        {
            _logFilePath = path;
        }

        public void Write(User user, string action, string result)
        {
            lock (_lockObject)
            {
                using (var writer = new StreamWriter(_logFilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ";" + user.Login + ";" + action + ";" + result);
                }
            }
        }
    }
}
