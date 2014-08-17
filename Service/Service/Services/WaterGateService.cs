using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using StaticValuesDll;

namespace Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WaterGateService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WaterGateRemoteService : IWaterGateService
    {
        private readonly Repository.Repository _repository = new Repository.Repository();


        public bool GetResponse()
        {
            return true;
        }

        public AuthorizationToken SignIn(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return new AuthorizationToken(Permissions.None, null);

            var permissions = _repository.GetPermissions(login, password);
            if(permissions == Permissions.None)
                return new AuthorizationToken(permissions, null);


            return new AuthorizationToken(permissions, _repository.GetConfigContainer());
        }

        public void UpdateCiscoRouters(List<IPCom> routers)
        {
            _repository.UpdateCiscoRouters(routers);
        }

        public void UpdatePorts(List<JDSUCiscoClass> ports)
        {
            _repository.UpdatePorts(ports);
        }

        public void UpdateJDSUIP(IPCom jdsuIP)
        {
            _repository.UpdateJDSUIP(jdsuIP);
        }
    }
}
