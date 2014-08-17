using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace StaticValuesDll
{
    [DataContract]
    public class User
    {
        [DataMember]
        public Int64 Id { get; set; }

        [DataMember]
        public Permissions Permissions { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
