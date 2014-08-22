using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Xml;
using StaticValuesDll;
using System.Threading;

namespace WaterGate
{
    



    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
        
           
            
            //  RegistryKey key = Registry.CurrentUser.CreateSubKey("WaterGate");
             //   key.SetValue("id", "sdsdsds");
             //   key.SetValue("auth", "sdsdsd");
             //   key.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
            Application.Exit();
         

            

        }
     }
}



