// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.forSerialize
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System;
using System.Xml.Serialization;

namespace StaticValuesDll
{
  [XmlRoot("WaterGate", IsNullable = false)]
  [Serializable]
  public class forSerialize
  {
    [XmlElement]
    public IPCom JDSUIP;
    [XmlElement]
    public int n;
    [XmlArrayItem("JDSUCisco")]
    [XmlArray("Ports")]
    public JDSUCiscoClass[] JDSUCiscoArray;
    [XmlArray("Cisco")]
    public IPCom[] CiscoList;
  }
}
