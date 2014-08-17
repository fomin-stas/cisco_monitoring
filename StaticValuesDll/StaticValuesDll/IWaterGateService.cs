using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StaticValuesDll;

namespace Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWaterGateService" in both code and config file together.
    [ServiceContract(Namespace = "WaterGateService")]
    public interface IWaterGateService
    {
        [OperationContract]
        bool GetResponse();

        [OperationContract]
        AuthorizationToken SignIn(string login, string password);

        [OperationContract]
        void UpdateConfig(ConfigContainer configContainer);

    }
}
