// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.IPCom
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
    public class IPCom
    {
        [XmlAttribute]
        [DataMember]
        public Int64 Id { get; set; }
        [XmlAttribute]
        [DataMember]
        public string IP { get; set; }
        [XmlAttribute]
        [DataMember]
        public string Com { get; set; }

        public IPCom()
        {
        }

        public IPCom(string ip, string com)
        {
            this.IP = ip;
            this.Com = com;
        }

        public override int GetHashCode()
        {
            return IP.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as IPCom;
            return item != null && IP.Equals(item.IP) && Com.Equals(item.Com);
        }
    }
}
