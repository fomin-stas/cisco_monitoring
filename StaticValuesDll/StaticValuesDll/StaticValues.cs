// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.StaticValues
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System.Collections.Generic;

namespace StaticValuesDll
{
  public static class StaticValues
  {
    public static JDSUCiscoClass[] JDSUCiscoArray = new JDSUCiscoClass[StaticValues.n];
    public static List<IPCom> CiscoList = new List<IPCom>();
    public static List<CiscoPort> PortList = new List<CiscoPort>();
    public static int n;
    public static IPCom JDSUIP;
  }
}
