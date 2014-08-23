using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using StaticValuesDll;

namespace Service
{

    class Functions
    {
        static object locker = new object();

        /// <summary>
        /// Reads config file and saves it to StaticValues.
        /// It is obsolete. Use config from database instead.
        /// </summary>
        /// <param name="path">Path to config file.</param>
        [Obsolete]
        public static void SerializeConfig(string path)
        {
            try
            {

                var file = new FileStream(path, FileMode.Open);

                var serializer = new XmlSerializer(typeof(StaticValuesDll.ConfigContainer));
                var ser = (StaticValuesDll.ConfigContainer)serializer.Deserialize(file);
                file.Close();

                if (ser.JDSUIP == null)
                {
                    AddTempLog(new List<string> { "Загружена некорректная конфигурация" });
   
                }
                else
                {
                    StaticValuesDll.StaticValues.JDSUIP = ser.JDSUIP;
                    StaticValuesDll.StaticValues.JDSUCiscoArray = ser.JDSUCiscoArray ?? new List<JDSUCiscoClass>();       
                }


            }
            catch (Exception ex)
            {

                AddTempLog(new List <string> {ex.Message, ex.ToString()});
                return;
            }

        }

        public static void AddTempLog(List <string> st)
        {
            lock (locker)
            {
                using (StreamWriter stream = new StreamWriter(pathLog, true))
                {
                    stream.WriteLine(DateTime.Now.ToString());
                    foreach (string k in st)
                    {
                        stream.WriteLine(k);
                    }
                    stream.WriteLine("\n");
                    stream.WriteLine("\n");
                    stream.WriteLine("\n");
                }
            }
        
        }

        public static void SendAlarm(String server, StaticValuesDll.AlarmClass alarm)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = null;

                try
                {
                    var formatter = new BinaryFormatter();
                    using (var ms = new MemoryStream())
                    {
                        using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                        {
                            formatter.Serialize(ds, alarm);
                        }
                        ms.Position = 0;
                        data = ms.GetBuffer();

                    }
                }
                catch (Exception ex)
                {
                    AddTempLog(new List<string> { ex.Message, ex.ToString() });/* handle exception omitted */
                }


                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

               
                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
               

                // Close everything.
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                AddTempLog(new List<string> {"ArgumentNullException: {0}", e.ToString(), e.Message});
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }

        public static string pathLog = Path.GetPathRoot(Environment.SystemDirectory) + @"WaterGateService\Servicelog.txt";

    }
}
