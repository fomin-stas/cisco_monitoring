using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using StaticValuesDll;

namespace Service.Repository
{
    public class Repository
    {
        public const string DefaultDatabaseFilePath = "C:\\Repository.db";
        private static string _connectionString;

        private static readonly ReaderWriterLockSlim LockSlim = new ReaderWriterLockSlim();

        public static void Initialize(string path)
        {
            _connectionString = "Data Source=" + path + ";Version=3;";
        }

        public ConfigContainer GetConfigContainer()
        {
            LockSlim.EnterReadLock();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    var container = new ConfigContainer();
                    using (var command = new SQLiteCommand("SELECT Id,JDSUPort,IP,Com,PortName,PortID FROM Ports", connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            using (var portsTable = new DataTable())
                            {
                                adapter.Fill(portsTable);

                                container.JDSUCiscoArray =
                                    portsTable.Rows.Cast<DataRow>().Select(item => new JDSUCiscoClass()
                                    {
                                        Id = (Int64) item[0],
                                        JDSUPort = item[1] as string,
                                        CiscoIPCom = new IPCom(item[2] as string, item[3] as string),
                                        CiscoPort = new CiscoPort(item[4] as string, item[5] as string)
                                    }).ToList();
                            }
                        }
                    }

                    container.JDSUIP = GetJDSUIp(connection);
                    if (container.JDSUIP == null)
                    {
                        using (var command = new SQLiteCommand("INSERT INTO JDSUIPAddresses (IP,Com) values(@ip,@com)", connection))
                        {
                            command.Parameters.Add("@ip", DbType.String).Value = string.Empty;
                            command.Parameters.Add("@com", DbType.String).Value = string.Empty;

                            command.ExecuteNonQuery();
                        }

                        container.JDSUIP = GetJDSUIp(connection);
                    }


                    using (var command = new SQLiteCommand("SELECT Id,IP,Com FROM CiscoRouters", connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            using (var ciscoTable = new DataTable())
                            {
                                adapter.Fill(ciscoTable);

                                container.CiscoList = ciscoTable.Rows.Cast<DataRow>().Select(item => new IPCom()
                                {
                                    Id = (Int64) item[0],
                                    IP = item[1] as string,
                                    Com = item[2] as string
                                }).ToList();
                            }
                        }
                    }

                    return container;
                }
            }
            finally { LockSlim.ExitReadLock(); }
        }

        public void UpdateConfig(ConfigContainer container)
        {
            LockSlim.EnterWriteLock();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("UPDATE JDSUIPAddresses SET IP=@ip,Com=@com", connection))
                    {
                        command.Parameters.Add("@ip", DbType.String).Value = container.JDSUIP.IP;
                        command.Parameters.Add("@com", DbType.String).Value = container.JDSUIP.Com;

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand("DELETE FROM Ports", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand("INSERT INTO Ports (JDSUPort,IP,Com,PortName,PortId) values(@port,@ip,@com,@portName,@portId)", connection))
                    {
                        command.Parameters.Add("@port", DbType.String);
                        command.Parameters.Add("@ip", DbType.String);
                        command.Parameters.Add("@com", DbType.String);
                        command.Parameters.Add("@portName", DbType.String);
                        command.Parameters.Add("@portId", DbType.String);

                        foreach (var item in container.JDSUCiscoArray)
                        {
                            command.Parameters["@port"].Value = item.JDSUPort;
                            command.Parameters["@ip"].Value = item.CiscoIPCom.IP;
                            command.Parameters["@com"].Value = item.CiscoIPCom.Com;
                            command.Parameters["@portName"].Value = item.CiscoPort.PortName;
                            command.Parameters["@portId"].Value = item.CiscoPort.PortID;

                            command.ExecuteNonQuery();
                        }
                    }

                    using (var command = new SQLiteCommand("INSERT INTO CiscoRouters (IP,Com) values(@ip,@com)", connection))
                    {
                        command.Parameters.Add("@ip", DbType.String);
                        command.Parameters.Add("@com", DbType.String);

                        foreach (var item in container.CiscoList)
                        {       
                            command.Parameters["@ip"].Value = item.IP;
                            command.Parameters["@com"].Value = item.Com;
                           
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }   
        }

        public Permissions GetPermissions(string login, string password)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand("SELECT Permissions.Value FROM Users  INNER JOIN Permissions ON Permissions.UserId=Users.Id WHERE Users.Login=@login AND Users.Password=@password", connection))
                {
                    command.Parameters.Add("@login", DbType.String).Value = login;
                    command.Parameters.Add("@password", DbType.String).Value = password.ToMD5();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (Permissions)reader.GetInt32(0);
                        }

                        return Permissions.None;
                    }
                }
            }
        }


        private IPCom GetJDSUIp(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("SELECT Id,IP,Com FROM JDSUIPAddresses", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new IPCom(reader.GetString(1), reader.GetString(2))
                        {
                            Id = reader.GetInt64(0)
                        };
                    }

                    return null;
                }
            }
        }
    }
}
