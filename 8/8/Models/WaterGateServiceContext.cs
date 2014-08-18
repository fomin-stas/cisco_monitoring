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
using System.Text;
using System.Xml.Serialization;
using Service.Services;
using StaticValuesDll;

namespace WaterGate.Models
{
    public class WaterGateServiceContext
    {
        private readonly string _address = Settings.WebServiceAddress + ":18285/WaterGateService/soap/";

        private BasicHttpBinding CreateBinding()
        {
            return new BasicHttpBinding()
            {
                MaxReceivedMessageSize = int.MaxValue
            };
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

                        continueWith(channel.GetResponse(), null);
                    }     
                }
                catch (Exception e)
                {
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

                        continueWith(channel.SignIn(login, password), null);
                    }          
                }
                catch (Exception e)
                {
                    continueWith(null, e);
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
                        channel.UpdatePorts(ports);

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
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
                        channel.UpdateJDSUIP(jdsuIP);

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
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
                        channel.UpdateCiscoRouters(routers);

                        continueWith(null);
                    }
                }
                catch (Exception e)
                {
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
                        var result = channel.AddUser(user);

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
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
                        var result = channel.RemoveUser(login);

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
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
                        var result = channel.GetUsers();

                        continueWith(result, null);
                    }
                }
                catch (Exception e)
                {
                    continueWith(null, e);
                }

            });

            asyncAction.BeginInvoke(null, null);
        }
    }
}
