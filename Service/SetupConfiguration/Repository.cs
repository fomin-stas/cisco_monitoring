using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Service;

namespace SetupConfiguration
{
    public class Repository
    {
        public static string DefaultDatabaseFilePath = Path.GetPathRoot(Environment.SystemDirectory) + "Repository.db";
        private static readonly string ConnectionString = "Data Source=" + DefaultDatabaseFilePath + ";Version=3;";

        public void UpdateServicePort(int port)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("UPDATE ServicePorts SET ServicePort=@port", connection))
                {
                    command.Parameters.Add("@port", DbType.Int32).Value = port;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateJdsu(string jdsuIp, string community)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("UPDATE JDSUIPAddresses SET IP=@ip,Com=@com", connection))
                {
                    command.Parameters.Add("@ip", DbType.String).Value = jdsuIp;
                    command.Parameters.Add("@com", DbType.String).Value = community;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddAdmin(string login, string password)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand("SELECT Id FROM Users WHERE Login=@login",
                    connection))
                {
                    command.Parameters.Add("@login", DbType.String).Value = login;

                    Int64 idForRemove = 0;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            idForRemove = reader.GetInt64(0);
                    }

                    if (idForRemove != 0)
                    {
                        command.CommandText = "DELETE FROM Users WHERE Login=@login";
                        command.ExecuteNonQuery();

                        command.Parameters.RemoveAt("@login");
                        command.CommandText = "DELETE FROM Permissions WHERE UserId=@id";
                        command.Parameters.Add("@id", DbType.Int64).Value = idForRemove;
                        command.ExecuteNonQuery();
                    }
                }

                Int64 id;
                using (var command = new SQLiteCommand("INSERT INTO Users (Login,Password) values(@login,@password)",
                        connection))
                {
                    command.Parameters.Add("@login", DbType.String).Value = login;
                    command.Parameters.Add("@password", DbType.String).Value = password.ToMD5();

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT Id FROM Users WHERE Login=@login";
                    command.Parameters.RemoveAt("@password");

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        id = reader.GetInt64(0);
                    }
                }


                using (var command = new SQLiteCommand("INSERT INTO Permissions (Value,UserId) values(@value,@userId)", connection))
                {
                    command.Parameters.Add("@value", DbType.Int32).Value = 1;
                    command.Parameters.Add("@userId", DbType.String).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
