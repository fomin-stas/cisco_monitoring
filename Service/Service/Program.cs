using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SnmpSharpNet;
using System.Threading;

namespace Service
{
 


    static class Program
    {
     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
          

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new WaterGate() 
            };
            ServiceBase.Run(ServicesToRun);
        }

       
       




    }
}
