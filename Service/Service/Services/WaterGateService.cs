using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Service.Models;
using StaticValuesDll;

namespace Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WaterGateService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WaterGateRemoteService : IWaterGateService
    {
        private readonly Repository.Repository _repository = new Repository.Repository();
        private readonly static ClientLogService ClientLogService = new ClientLogService();

        public bool GetResponse()
        {
            return true;
        }

        public AuthorizationToken SignIn(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ClientLogService.Write(CurrentUser, "Авторизация", "В доступе отказано. Пустой логин либо пароль");
                return new AuthorizationToken(new User() {Permissions = Permissions.None}, null);
            }


            var user = _repository.LogUserIn(login, password);
            if (user.Permissions == Permissions.None)
            {
                ClientLogService.Write(CurrentUser, "Авторизация",
                    "В доступе отказано. Пользователь с данной комбинацией логина и пароля не существует. Логин: " +
                    login);
                return new AuthorizationToken(user, null);
            }


            var result = new AuthorizationToken(user, _repository.GetConfigContainer());
            ClientLogService.Write(CurrentUser, "Авторизация", "Доступ разрешен. Логин: " + login);
            return result;
        }

        public void UpdateCheckDelay(double delay)
        {
            _repository.UpdateCheckDelay(delay);
            ClientLogService.Write(CurrentUser, "Изменение интервала проверки", "Интервал изменен на " + delay + " мин.");
        }

        public void UpdatePortDescription(JDSUCiscoClass jdsuCisco)
        {
            _repository.UpdatePortDescription(jdsuCisco);
            ClientLogService.Write(CurrentUser, "Изменение описания порта", " Описание порта " + jdsuCisco.JDSUPort + " " + jdsuCisco.CiscoIPCom.IP + " " + jdsuCisco.CiscoIPCom.Com + " " +
                jdsuCisco.CiscoPort.PortName + " " + jdsuCisco.CiscoPort.PortID + " изменено на: " + jdsuCisco.Description);
        }

        public void UpdateCiscoRouters(List<IPCom> routers)
        {
            _repository.UpdateCiscoRouters(routers);
            ClientLogService.Write(CurrentUser, "Обновление Cisco адресов", "Обновлено");
        }

        public void UpdatePorts(List<JDSUCiscoClass> ports)
        {
            _repository.UpdatePorts(ports);
            ClientLogService.Write(CurrentUser, "Обновление портов", "Обновлено");
        }

        public void UpdateJDSUIP(IPCom jdsuIP)
        {
            _repository.UpdateJDSUIP(jdsuIP);
            ClientLogService.Write(CurrentUser, "Обновление JDSUIP", "Обновлено на " + jdsuIP.IP + " " + jdsuIP.Com);
        }

        public bool AddUser(User user)
        {
            var result = _repository.AddUser(user);
            ClientLogService.Write(CurrentUser, "Добавление пользователя", "Пользователь " + user.Login + (result ? " добавлен" : "не добавлен. Некорректный логин"));
            return result;
        }

        public bool RemoveUser(string login)
        {
           var result = _repository.RemoveUser(login);
           ClientLogService.Write(CurrentUser, "Удаление пользователя", "Пользователь " + login + (result ? " удален" : "не найден"));
           return result;
        }

        public void LogUnlockingPort(JDSUCiscoClass jdsuCisco, UnlockingPortStatus unlockingPortStatus)
        {
            var portString = "Порт " + jdsuCisco.JDSUPort + " " + jdsuCisco.CiscoIPCom.IP + " " + jdsuCisco.CiscoIPCom.Com + " " + jdsuCisco.CiscoPort.PortName + ".";
            switch (unlockingPortStatus)
            {
                case UnlockingPortStatus.Starting:
                {
                    ClientLogService.Write(CurrentUser, "Начало разблокировки порта", portString + " Выполнено");
                    return;
                }
                case UnlockingPortStatus.InvalidSmnp:
                {
                    ClientLogService.Write(CurrentUser, "Начало разблокировки порта", portString + " Некорректный адрес SMNP");
                    return;
                }
                case UnlockingPortStatus.NoResponse:
                {
                    ClientLogService.Write(CurrentUser, "Разблокировки порта", portString + " Нет ответа");
                    return;
                }
                case UnlockingPortStatus.NotActive:
                {
                    ClientLogService.Write(CurrentUser, "Разблокировки порта", portString + " Порт не разблокирован");
                    return;
                }
                case UnlockingPortStatus.Active:
                {
                    ClientLogService.Write(CurrentUser, "Разблокировки порта", portString + " Порт разблокирован");
                    return;
                }
            }
            
        }

        public User[] GetUsers()
        {
            var result = _repository.GetUsers();
            ClientLogService.Write(CurrentUser, "Получение списка пользователей", "Выполнено");
            return result;
        }

        public User CurrentUser
        {
            get
            {
                return OperationContext.Current.IncomingMessageHeaders.FindHeader("user", "WaterGate") > -1
                    ? OperationContext.Current.IncomingMessageHeaders.GetHeader<User>("user", "WaterGate")
                    : new User(){Login = "UNKNOWN"};
            }
        }
    }
}
