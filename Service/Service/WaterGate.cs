using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using Service.Models;
using Service.Services;
using SnmpSharpNet;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO.Compression;
using StaticValuesDll;
using System.Data.SQLite;
using System.Data.Common;

namespace Service
{
 
    public partial class WaterGate : ServiceBase
    {
        private readonly Thread workerThread;
        private readonly Thread TCPlistenerthread;
        private static string LogFilePath = Path.GetPathRoot(Environment.SystemDirectory) + "WaterGateService\\Temp.txt";

        static WaterGate()
        {
            try
            {
                var path = Path.GetPathRoot(Environment.SystemDirectory) + "WaterGateService";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch { }
        }
        
        public WaterGate()
        {
            InitializeComponent();

            workerThread = new Thread(DoWork)
            {
                IsBackground = true
            };

            TCPlistenerthread = new Thread(TCPlistener)
            {
                IsBackground = true
            };
        }


        protected override void OnStart(string[] args)
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Launch();
#endif


            Repository.Repository.Initialize(CommandLineHelper.GetConfigFilePath(args) ?? Repository.Repository.DefaultDatabaseFilePath);
            ClientLogService.Initialize(CommandLineHelper.GetLogFilePath(args) ?? ClientLogService.DefaultLogFilePath);

            AddLog("WaterGate Service started");
            using (StreamWriter stream = new StreamWriter(LogFilePath, true))
            {
                stream.WriteLine("Служба запущена!");
                stream.WriteLine(DateTime.Now);
            }

            workerThread.Start();
            TCPlistenerthread.Start();

            var repository = new Repository.Repository();

            var port = repository.GetPortNumber();
            var host = new ServiceHost(typeof(WaterGateRemoteService), new Uri("http://localhost:" + port + "/WaterGateService/soap"));
            var binding = new BasicHttpBinding(){ MaxReceivedMessageSize = int.MaxValue };

            host.AddServiceEndpoint(typeof(IWaterGateService), binding, string.Empty);
            host.Open();
        }


        protected override void OnStop()
        {
            AddLog("Service is stopped");
            using (StreamWriter stream = new StreamWriter(LogFilePath, true))
            {
                stream.WriteLine("Служба остановлена!");
                stream.WriteLine(DateTime.Now);
            }
            workerThread.Abort();
            TCPlistenerthread.Abort();

        }

        private static void TCPlistener()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 32157);
            EndPoint ep = (EndPoint)ipep;
            socket.Bind(ep);
            // Disable timeout processing. Just block until packet is received 
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);

            while (true)
            {
                byte[] indata = new byte[16 * 1024];
                // 16KB receive buffer 
                int inlen = 0;
                IPEndPoint peer = new IPEndPoint(IPAddress.Any, 0);
                EndPoint inep = (EndPoint)peer;
                try
                {
                    inlen = socket.ReceiveFrom(indata, ref inep);
                }
                catch (Exception ex)
                {
                    Functions.AddTempLog(new List<string> { ex.Message, ex.ToString() });
                   
                    inlen = -1;
                }
                if (inlen > 0)
                {
                    try
                    {
                        StaticValuesDll.JDSUCiscoClass ser = new StaticValuesDll.JDSUCiscoClass();
                        ser = null;
                      
                        var formatter = new BinaryFormatter();
                        using (var ms = new MemoryStream(indata))
                        {
                            using (var ds = new DeflateStream(ms, CompressionMode.Decompress, true))
                            {
                                ser = (StaticValuesDll.JDSUCiscoClass)formatter.Deserialize(ds);
                            }
                        }

                        Functions.AddTempLog(new List<string> { ser.JDSUPort });
                    }
                    catch (Exception ex)
                    {
                        Functions.AddTempLog(new List<string> { ex.Message, ex.ToString() });
                    }


                   Functions.AddTempLog(new List <string>  {Encoding.ASCII.GetString(indata)});
                    
                
                }
            }
        
        }


        private static void DoWork()
        {


            // Construct a socket and bind it to the trap manager port 162 

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
            EndPoint ep = (EndPoint)ipep;
            socket.Bind(ep);
            // Disable timeout processing. Just block until packet is received 
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);

            while (true)
            {
                byte[] indata = new byte[16 * 1024];
                // 16KB receive buffer 
                int inlen = 0;
                IPEndPoint peer = new IPEndPoint(IPAddress.Any, 0);
                EndPoint inep = (EndPoint)peer;
                try
                {
                    inlen = socket.ReceiveFrom(indata, ref inep);
                }
                catch (Exception ex)
                {
                    Functions.AddTempLog(new List<string> { ex.Message, ex.ToString() });
                    inlen = -1;
                }
                if (inlen > 0)
                {
                    // Check protocol version 
                    int ver = SnmpPacket.GetProtocolVersion(indata, inlen);
                    if (ver == (int)SnmpVersion.Ver2)
                    {


                        // Parse SNMP Version 2 TRAP packet 
                        SnmpV2Packet pkt = new SnmpV2Packet();
                        pkt.decode(indata, inlen);
                        List<string> alarm = new List<string> { inep.ToString(), pkt.Community.ToString() };
                        List<string> trap = new List<string>();
                        foreach (Vb v in pkt.Pdu.VbList)
                        {
                            trap.Add(v.Value.ToString());
                        }


                        if (trap.Count == 42)
                        {
                            IPCom _ipcom = new IPCom();
                            CiscoPort _ciscoPort = new CiscoPort();

                            string path = "C:\\Repository.db";
                            string _connectionString;
                            _connectionString = "Data Source=" + path + ";Version=3;";
                            using (var connection = new SQLiteConnection(_connectionString))
                            {
                                connection.Open();

                                using (var command = new SQLiteCommand("SELECT IP,Com,PortId FROM Ports WHERE JDSUPort = @_JDSUPort", connection))
                                {
                                    command.Parameters.Add("@_JDSUPort", DbType.String).Value = trap[27];

                                    try
                                    {
                                        using (var reader = command.ExecuteReader())
                                        {

                                            foreach (DbDataRecord record in reader)
                                            {
                                                SimpleSnmp snmp = new SimpleSnmp(record["IP"].ToString(), record["Com"].ToString());
                                               
                                                
                                                //надеюсь, если что упадет в exception
                                                /*if (!snmp.Valid)
                                                {
                                                   
                                                    return;
                                                }*/

                                                Pdu pdu = new Pdu(PduType.Set);
                                                pdu.VbList.Add(new Oid(".1.3.6.1.2.1.2.2.1.7" + record["PortId"].ToString()), new Integer32(0));
                                                snmp.Set(SnmpVersion.Ver2, pdu);



                                                alarm.Add(record["IP"].ToString());
                                                alarm.Add(record["Com"].ToString());
                                                alarm.Add(record["PortId"].ToString());
                                            }

                                        }

                                       


                                    }

                                    catch (Exception ex)
                                    {
                                        alarm.Add(ex.ToString());
                                        alarm.Add("в базе данных нет такой записи");
                                    }


                                }

                            }

                        }
                        else
                        {
                            if (inlen == 0)
                            {
                                Functions.AddTempLog(new List<string> { "Zero length packet received." });
                            }


                        }

                    }
                }
            }
        }

        
            


        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("WaterGateService"))
                {
                    EventLog.CreateEventSource("WaterGateService", "WaterGateService");
                }

                eventLog1.Source = "WaterGateservice";
                eventLog1.WriteEntry(log);
            }
            catch { }
        }

       
    }
}
