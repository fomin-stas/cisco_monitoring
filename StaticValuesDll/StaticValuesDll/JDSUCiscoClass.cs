// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.JDSUCiscoClass
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace StaticValuesDll
{
    [Serializable]
    [DataContract]
    public class JDSUCiscoClass
    {
        [XmlAttribute]
        [DataMember]
        public Int64 Id { get; set; }

        [XmlElement]
        [DataMember]
        public string JDSUPort { get; set; }

        [XmlElement]
        [DataMember]
        public IPCom CiscoIPCom { get; set; }

        [XmlElement]
        [DataMember]
        public CiscoPort CiscoPort { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Note { get; set; }

        public void AddJDSUCisco(string jdsuPort, IPCom ciscoIP, CiscoPort ciscoPort)
        {
            this.JDSUPort = jdsuPort;
            this.CiscoIPCom = ciscoIP;
            this.CiscoPort = ciscoPort;
        }
    }
}
