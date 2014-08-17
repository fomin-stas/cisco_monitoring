using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using StaticValuesDll;

namespace WaterGate
{
    public class Functions
    {
        ///Resets StaticValues and saves config.

        //public static void renew()
        //{
        //    StaticValues.n = 48;
        //    StaticValues.JDSUIP = new IPCom("not set", "not set");
        //    Array.Resize(ref StaticValues.JDSUCiscoArray, StaticValues.n);
        //    for (int i = 0; i < StaticValues.n; i++)
        //    {
        //        StaticValues.JDSUCiscoArray[i] = new JDSUCiscoClass();
        //        StaticValues.JDSUCiscoArray[i].AddJDSUCisco(i, "not set", new IPCom("not set", "CiscoCom"), new CiscoPort("not set", "PortID")); 
        //    }

            
        //    StaticValues.CiscoList.Add(new IPCom("not set","ciscoCom"));
            
           
        //    forSerialize ser = new forSerialize();

        //    ser.n = StaticValues.n;
        //    ser.JDSUIP = StaticValues.JDSUIP;
        //    ser.JDSUCiscoArray = StaticValues.JDSUCiscoArray;
        //    ser.CiscoList = StaticValues.CiscoList.ToArray();

        //    FileStream myFileStream = new FileStream(pathConfig, FileMode.Create);  
        //    XmlSerializer serializer = new XmlSerializer(typeof(forSerialize));
            
        //    serializer.Serialize(myFileStream,ser);
        //    return;
        
        //}

        public static void AddTempLog(string st, string st2 = "")
        {
            
            using (StreamWriter stream = new StreamWriter(pathLog, true))
            {
                stream.WriteLine(DateTime.Now.ToString());
                stream.WriteLine(st);
                stream.WriteLine(st2);
                stream.WriteLine("\n");
                stream.WriteLine("\n");
                stream.WriteLine("\n");
    
            
            }

        }

        public static void SerializeConfig(string path)
        {
            try
            {

                FileStream file = new FileStream(path, FileMode.Open);

                ConfigContainer ser = new ConfigContainer();
                ser = null;
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigContainer));
                ser = (ConfigContainer)serializer.Deserialize(file);
                file.Close();

                // ser.n == ser.JDSUCiscoArray.Count()
                if (ser.JDSUIP == null)
                {
                    MessageBox.Show("Загружена некорректная конфигурация");
                }
                else
                {
                    StaticValues.JDSUIP = ser.JDSUIP;
                    StaticValues.JDSUCiscoArray = ser.JDSUCiscoArray;
                    StaticValues.CiscoList = new List<IPCom>(ser.CiscoList);
                }
    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
           
        }
     
      

        public static string pathConfig = @"C:\program1\Service\config.xml";
        public static string pathLog = @"C:\program1\Service\UsersApplog.txt";
       
        


    }




}

