using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using StaticValuesDll;

namespace Service.Repository
{
    public class Repository
    {
        private const double DefaultCheckDelay = 0.5;
        private const int DefaultServicePortNumber = 18285;

        public static string DefaultDatabaseFilePath = Path.GetPathRoot(Environment.SystemDirectory) + "Repository.db";
        private static string _connectionString;

        private static readonly ReaderWriterLockSlim LockSlim = new ReaderWriterLockSlim();
        private static readonly ReaderWriterLockSlim UsersLockSlim = new ReaderWriterLockSlim();

        public static void Initialize(string path)
        {
            _connectionString = "Data Source=" + path + ";Version=3;";
        }

        public int GetPortNumber()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand("SELECT ServicePort FROM ServicePorts", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.Read() ? reader.GetInt32(0) : DefaultServicePortNumber;
                    }
                }
            }
        }

        public void UpdateCheckDelay(double delay)
        {
            LockSlim.TryEnterWriteLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT Value FROM Settings WHERE Key=@key", connection))
                    {
                        command.Parameters.Add("@key", DbType.String).Value = "CheckDelay";

                        var shouldUpdate = false;
                        using (var reader = command.ExecuteReader())
                        {
                            command.Parameters.Add("@value", DbType.String).Value = delay.ToString(CultureInfo.InvariantCulture);

                            if (reader.Read())
                            {
                                shouldUpdate = true; 
                            }
                        }

                        if (shouldUpdate)
                        {
                            command.CommandText = "UPDATE Settings SET Value=@value WHERE Key=@key";
                            command.ExecuteNonQuery();
                            return;
                        }

                        command.CommandText = "INSERT INTO Settings (Key,Value) values(@key,@value)";
                        command.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }

        public ConfigContainer GetConfigContainer()
        {
            LockSlim.TryEnterReadLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    var container = new ConfigContainer();
                    using (var command = new SQLiteCommand("SELECT Id,JDSUPort,IP,Com,PortName,PortID,Description,Note FROM Ports", connection))
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
                                        CiscoPort = new CiscoPort(item[4] as string, item[5] as string),
                                        Description = item[6] as string,
                                        Note = item[7] as string
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

                    container.CheckDelay = GetCheckDelay(connection);

                    return container;
                }
            }
            finally { LockSlim.ExitReadLock(); }
        }

        public void UpdateJDSUIP(IPCom jdsuIP)
        {
            LockSlim.TryEnterWriteLock(-1);
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("UPDATE JDSUIPAddresses SET IP=@ip,Com=@com", connection))
                    {
                        command.Parameters.Add("@ip", DbType.String).Value = jdsuIP.IP;
                        command.Parameters.Add("@com", DbType.String).Value = jdsuIP.Com;

                        command.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }

        public void UpdatePortDescription(JDSUCiscoClass jdsuCisco)
        {
            LockSlim.TryEnterWriteLock(-1);
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand( "UPDATE Ports SET Description=@description WHERE IP=@ip AND Com=@com AND PortName=@portName AND PortId=@portId", connection))
                    {
                        command.Parameters.Add("@description", DbType.String).Value = jdsuCisco.Description;
                        command.Parameters.Add("@ip", DbType.String).Value = jdsuCisco.CiscoIPCom.IP;
                        command.Parameters.Add("@com", DbType.String).Value = jdsuCisco.CiscoIPCom.Com;
                        command.Parameters.Add("@portName", DbType.String).Value = jdsuCisco.CiscoPort.PortName;
                        command.Parameters.Add("@portId", DbType.String).Value = jdsuCisco.CiscoPort.PortID;

                        command.ExecuteNonQuery();
                    }

                }
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }

        public void UpdatePortNote(JDSUCiscoClass jdsuCisco)
        {
            LockSlim.TryEnterWriteLock(-1);
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("UPDATE Ports SET Note=@note WHERE IP=@ip AND Com=@com AND PortName=@portName AND PortId=@portId", connection))
                    {
                        command.Parameters.Add("@note", DbType.String).Value = jdsuCisco.Note;
                        command.Parameters.Add("@ip", DbType.String).Value = jdsuCisco.CiscoIPCom.IP;
                        command.Parameters.Add("@com", DbType.String).Value = jdsuCisco.CiscoIPCom.Com;
                        command.Parameters.Add("@portName", DbType.String).Value = jdsuCisco.CiscoPort.PortName;
                        command.Parameters.Add("@portId", DbType.String).Value = jdsuCisco.CiscoPort.PortID;

                        command.ExecuteNonQuery();
                    }

                }
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }

        public void UpdatePorts(List<JDSUCiscoClass> ports)
        {
            LockSlim.TryEnterWriteLock(-1);
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("DELETE FROM Ports", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand( "INSERT INTO Ports (JDSUPort,IP,Com,PortName,PortId,Description) values(@port,@ip,@com,@portName,@portId,@description)", connection))
                    {
                        command.Parameters.Add("@port", DbType.String);
                        command.Parameters.Add("@ip", DbType.String);
                        command.Parameters.Add("@com", DbType.String);
                        command.Parameters.Add("@portName", DbType.String);
                        command.Parameters.Add("@portId", DbType.String);
                        command.Parameters.Add("@description", DbType.String);

                        foreach (var item in ports)
                        {
                            command.Parameters["@port"].Value = item.JDSUPort;
                            command.Parameters["@ip"].Value = item.CiscoIPCom.IP;
                            command.Parameters["@com"].Value = item.CiscoIPCom.Com;
                            command.Parameters["@portName"].Value = item.CiscoPort.PortName;
                            command.Parameters["@portId"].Value = item.CiscoPort.PortID;
                            command.Parameters["@description"].Value = item.Description;

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

        public void UpdateCiscoRouters(List<IPCom> routers)
        {
            LockSlim.TryEnterWriteLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("DELETE FROM CiscoRouters", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SQLiteCommand("INSERT INTO CiscoRouters (IP,Com) values(@ip,@com)", connection))
                    {
                        command.Parameters.Add("@ip", DbType.String);
                        command.Parameters.Add("@com", DbType.String);

                        foreach (var item in routers)
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

        public User LogUserIn(string login, string password)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand("SELECT Users.Id,Users.Login,Permissions.Value FROM Users  INNER JOIN Permissions ON Permissions.UserId=Users.Id WHERE Users.Login=@login AND Users.Password=@password", connection))
                {
                    command.Parameters.Add("@login", DbType.String).Value = login;
                    command.Parameters.Add("@password", DbType.String).Value = password.ToMD5();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User()
                            {
                                Id = reader.GetInt64(0),
                                Login = reader.GetString(1),
                                Permissions = (Permissions)reader.GetInt32(2)
                            };
                        }

                        return new User()
                        {
                            Permissions = Permissions.None
                        };
                    }
                }
            }
        }

        public bool AddUser(User user)
        {
            UsersLockSlim.TryEnterWriteLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    Int64 id;
                    try
                    {
                        using (var command =new SQLiteCommand("INSERT INTO Users (Login,Password) values(@login,@password)", connection))
                        {
                            command.Parameters.Add("@login", DbType.String).Value = user.Login;
                            command.Parameters.Add("@password", DbType.String).Value = user.Password.ToMD5();

                            command.ExecuteNonQuery();
                        }

                        id = GetUserId(connection, user.Login);
                        if (id == 0)
                            return false;
                    }
                    catch
                    {
                        return false;
                    }

                    using (var command = new SQLiteCommand("INSERT INTO Permissions (Value,UserId) values(@value,@userId)", connection))
                    {
                        command.Parameters.Add("@value", DbType.Int32).Value = (int) user.Permissions;
                        command.Parameters.Add("@userId", DbType.String).Value = id;

                        command.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            finally { UsersLockSlim.ExitWriteLock();}
        }

        public bool RemoveUser(string login)
        {
            UsersLockSlim.TryEnterWriteLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    var userId = GetUserId(connection, login);
                    if (userId == 0)
                        return true;

                    var canRemoveAdministrator = true;
                    using (var command =new SQLiteCommand("SELECT count(Users.Id) FROM Users INNER JOIN Permissions ON Users.Id=Permissions.UserId WHERE Permissions.Value=1", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                canRemoveAdministrator = Convert.ToInt32(reader.GetValue(0)) > 1;
                            }
                        }
                    }

                    using (var command = new SQLiteCommand("SELECT Value FROM Permissions WHERE UserId=@id", connection))
                    {
                        command.Parameters.Add("@id", DbType.Int64).Value = userId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                return true;
                            }

                            var permissions = reader.GetInt32(0);
                            if (permissions == 1 && !canRemoveAdministrator)
                                return false;
                        }
                    }

                    using (var command = new SQLiteCommand("DELETE FROM Permissions WHERE UserId=@id", connection))
                    {
                        command.Parameters.Add("@id", DbType.Int64).Value = userId;
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand("DELETE FROM Users WHERE Id=@id", connection))
                    {
                        command.Parameters.Add("@id", DbType.Int64).Value = userId;
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            finally
            {
                UsersLockSlim.ExitWriteLock();
            }
        }

        public User[] GetUsers()
        {
            UsersLockSlim.TryEnterReadLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT Users.Id,Users.Login,Permissions.Value FROM Users INNER JOIN Permissions ON Users.Id=Permissions.UserId", connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            using (var table = new DataTable())
                            {
                                adapter.Fill(table);

                                return table.Rows.Cast<DataRow>().Select(item => new User()
                                {
                                    Id = (Int64) item[0],
                                    Login = item[1] as string,
                                    Permissions = (Permissions) (int) item[2]
                                }).ToArray();
                            }
                        }
                    }
                }
            }
            finally
            {
                UsersLockSlim.ExitReadLock();
            }
        }

        private double GetCheckDelay(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("SELECT Value FROM Settings WHERE Key=@key", connection))
            {
                command.Parameters.Add("@key", DbType.String).Value = "CheckDelay";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        try
                        {
                            return double.Parse(reader.GetString(0), CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            command.CommandText = "DELETE FROM Settings WHERE Key=@key";
                            command.ExecuteNonQuery();
                        }
                    }

                    return DefaultCheckDelay;
                }
            }
        }

        public AlarmList[] GetAlarmList()
        {
            LockSlim.TryEnterReadLock(-1);


            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT AlarmList.Id,AlarmList.Name,AlarmList.Execute FROM AlarmList", connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            using (var table = new DataTable())
                            {
                                adapter.Fill(table);




                                return table.Rows.Cast<DataRow>().Select(item => new AlarmList()
                                {
                                    Id = (Int64)item[0],
                                    Name = item[1] as string,
                                    Execute = (int)item[2]


                                }).ToArray();


                            }
                        }
                    }
                }
            }
            finally
            {
                LockSlim.ExitReadLock();
            }
        }


        public AlarmLevelList[] GetAlarmLevelList()
        {
            LockSlim.TryEnterReadLock(-1);


            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT AlarmLevelList.Id,AlarmLevelList.Level,AlarmLevelList.Execute FROM AlarmLevelList", connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            using (var table = new DataTable())
                            {
                                adapter.Fill(table);




                                return table.Rows.Cast<DataRow>().Select(item => new AlarmLevelList()
                                {
                                    Id = (Int64)item[0],
                                    Level = (Int64)item[1],
                                    Execute = (int)item[2]


                                }).ToArray();


                            }
                        }
                    }
                }
            }
            finally
            {
                LockSlim.ExitReadLock();
            }
        }

        public bool ChangeAlarm(StaticValuesDll.AlarmList alarm)
        {
            LockSlim.TryEnterWriteLock(-1);

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();



                    using (var command = new SQLiteCommand("UPDATE AlarmList SET Execute=@Execute WHERE Id=@id AND Name=@name", connection))
                    {
                        command.Parameters.Add("@Execute", DbType.Int32).Value = alarm.Execute;
                        command.Parameters.Add("@id", DbType.Int32).Value = alarm.Id;
                        command.Parameters.Add("@name", DbType.String).Value = alarm.Name;

                        command.ExecuteNonQuery();
                    }


                    return true;
                }
            }
            catch
            
            {
                return false;
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }
        private Int64 GetUserId(SQLiteConnection connection, string login)
        {
            using (var command = new SQLiteCommand("SELECT Id FROM Users WHERE Login=@login", connection))
            {
                command.Parameters.Add("@login", DbType.String).Value = login;

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return 0;
                    }

                    return reader.GetInt64(0);
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
