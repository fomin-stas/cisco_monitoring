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
        void UpdateCiscoRouters(List<IPCom> routers);

        [OperationContract]
        void UpdatePorts(List<JDSUCiscoClass> ports);

        [OperationContract]
        void UpdateJDSUIP(IPCom jdsuIP);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool RemoveUser(string login);

        [OperationContract]
        void LogUnlockingPort(JDSUCiscoClass jdsuCisco, UnlockingPortStatus unlockingPortStatus);

        [OperationContract]
        User[] GetUsers();

        [OperationContract]
        AlarmLevelList[] GetAlarmLevelList();

        [OperationContract]
        AlarmList[] GetAlarmList();

        [OperationContract]
        void UpdateCheckDelay(double delay);

        [OperationContract]
        void UpdatePortDescription(JDSUCiscoClass jdsuCisco);

        [OperationContract]
        void UpdatePortNote(JDSUCiscoClass jdsuCisco);
    }
}
