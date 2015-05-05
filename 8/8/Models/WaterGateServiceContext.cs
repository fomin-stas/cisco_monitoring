using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml.Serialization;
using Service.Services;
using StaticValuesDll;

namespace WaterGate.Models
{
    public class WaterGateServiceContext
    {
        private readonly string _address = Settings.WebServiceAddress + "/WaterGateService/soap/";
        private static MessageHeader _userHeader;

        private BasicHttpBinding CreateBinding()
        {
            return new BasicHttpBinding()
            {
                MaxReceivedMessageSize = int.MaxValue
            };
        }

        protected void LogConnectionFailed()
        {
            Functions.AddTempLog("Соединение с сервером не установлено.");
        }

        public void GetResponseAsync(Action<bool, Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            continueWith(channel.GetResponse(), null);
                        }
                    }     
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(false, e);
                }
            });

            asyncAction.BeginInvoke(null, null);
        }

        public void SignInAsync(string login, string password, Action<AuthorizationToken, Exception> continueWith)
        {

            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        var authorizationToken = channel.SignIn(login, password);
                        using (var contextScope = new OperationContextScope((IContextChannel)channel))
                        {
                            _userHeader = MessageHeader.CreateHeader("user", "WaterGate", authorizationToken.User);
                        }

                        continueWith(authorizationToken, null);
                    }          
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(null, e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void UpdatePortDescriptionAsync(JDSUCiscoClass jdsuCisco, Action<Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel)channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.UpdatePortDescription(jdsuCisco);
                        }

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void UpdatePortNoteAsync(JDSUCiscoClass jdsuCisco, Action<Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel)channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.UpdatePortNote(jdsuCisco);
                        }

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void UpdateCheckDelayAsync(double delay, Action<Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel)channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.UpdateCheckDelay(delay);
                        }

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void UpdatePorts(List<JDSUCiscoClass> ports, Action<Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.UpdatePorts(ports);
                        }

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void UpdateJDSUIP(IPCom jdsuIP, Action<Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.UpdateJDSUIP(jdsuIP);
                        }

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }


        public void UpdateCiscoRouters(List<IPCom> routers, Action<Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.UpdateCiscoRouters(routers);
                        }

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void AddUserAsync(StaticValuesDll.User user, Action<bool, Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        bool result;
                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            result = channel.AddUser(user);
                        }

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(false, e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void RemoveUserAsync(string login, Action<bool, Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        bool result;
                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            result = channel.RemoveUser(login);
                        }

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(false, e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }

        public void GetUsersAsync(Action<StaticValuesDll.User[], Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        User[] result;
                        using (var contextScope = new OperationContextScope((IContextChannel)channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            result = channel.GetUsers();    
                        }

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(null, e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }


        public void GetAlarmAsync(Action<StaticValuesDll.AlarmList[], Exception> continueWith)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(), new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        AlarmList[] result;
                        using (var contextScope = new OperationContextScope((IContextChannel)channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            result = channel.GetAlarmList();
                        }

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
                    LogConnectionFailed();
                    continueWith(null, e);
                }

            });

            asyncAction.BeginInvoke(null, null);

        }


        public void LogUnlockingPortAsync(JDSUCiscoClass jdsuCisco, UnlockingPortStatus unlockingPortStatus)
        {
            var asyncAction = new Action(() =>
            {
                try
                {
                    using (
                        var serviceChannel = new ChannelFactory<IWaterGateService>(CreateBinding(),
                            new EndpointAddress(_address)))
                    {
                        var channel = serviceChannel.CreateChannel();

                        using (var contextScope = new OperationContextScope((IContextChannel) channel))
                        {
                            OperationContext.Current.OutgoingMessageHeaders.Add(_userHeader);
                            channel.LogUnlockingPort(jdsuCisco, unlockingPortStatus);
                        }
                    }
                }
                catch
                {
                    LogConnectionFailed();
                }
            });
            asyncAction.BeginInvoke(null, null);
        }

    }
}
