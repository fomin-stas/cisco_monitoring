// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.IPCom
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System;
using System.Xml.Serialization;

namespace StaticValuesDll
{
  [Serializable]
  public class IPCom
  {
    [XmlAttribute]
    public string IP;
    [XmlAttribute]
    public string Com;

    public IPCom()
    {
    }

    public IPCom(string ip, string com)
    {
      this.IP = ip;
      this.Com = com;
    }
  }
}
