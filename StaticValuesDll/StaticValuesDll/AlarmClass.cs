// Decompiled with JetBrains decompiler
// Type: StaticValuesDll.AlarmClass
// Assembly: StaticValuesDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1FE7C1BF-C832-4369-A259-23E6507E7602
// Assembly location: C:\Users\Vladimir\Desktop\cisco_monitoring-master\8\8\bin\Debug\StaticValuesDll.dll

using System;
using System.Xml.Serialization;

namespace StaticValuesDll
{
    [Serializable]
    public class AlarmClass
    {
        [XmlElement]
        public int ClearStatus;
        [XmlElement]
        public int InternalKeyAlarm;
        [XmlElement]
        public IPCom inep;
        [XmlElement]
        public DateTime DateTime;
        [XmlElement]
        public string link;
        [XmlElement]
        public string SpecificProblem;
        [XmlElement]
        public string AdditionalText;
    }
}
