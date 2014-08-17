// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.CiscoPort
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System;
using System.Xml.Serialization;

namespace StaticValuesDll
{
  [Serializable]
  public class CiscoPort
  {
    [XmlAttribute]
    public string PortName;
    [XmlAttribute]
    public string PortID;

    public CiscoPort()
    {
    }

    public CiscoPort(string portname, string portid)
    {
      this.PortName = portname;
      this.PortID = portid;
    }
  }
}
