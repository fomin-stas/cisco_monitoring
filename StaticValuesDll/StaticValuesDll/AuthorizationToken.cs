using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace StaticValuesDll
{
    public enum Permissions
    {
        Administrator = 1,
        User = 2,
        None = 0
    }

    [DataContract]
    public class AuthorizationToken
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public ConfigContainer ConfigContainer { get; set; }

        public AuthorizationToken() { }

        public AuthorizationToken(User user, ConfigContainer configContainer)
        {
            User = user;
            ConfigContainer = configContainer;
        }
    }
}
