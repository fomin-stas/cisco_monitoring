// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.JDSUCiscoClass
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System;
using System.Xml.Serialization;

namespace StaticValuesDll
{
  [Serializable]
  public class JDSUCiscoClass
  {
    [XmlAttribute]
    public int n;
    [XmlElement]
    public string JDSUPort;
    [XmlElement]
    public IPCom CiscoIPCom;
    [XmlElement]
    public CiscoPort CiscoPort;

    public void AddJDSUCisco(int N, string jdsuPort, IPCom ciscoIP, CiscoPort ciscoPort)
    {
      this.n = N;
      this.JDSUPort = jdsuPort;
      this.CiscoIPCom = ciscoIP;
      this.CiscoPort = ciscoPort;
    }
  }
}
