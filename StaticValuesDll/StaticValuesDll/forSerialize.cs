using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StaticValuesDll
{
    [Serializable()]
    [XmlRoot("WaterGate", IsNullable = false)]
    public class forSerialize
    {
        [XmlElement]
        public IPCom JDSUIP;
        [XmlElement]
        public int n;
        [XmlArray("Ports")]
        [XmlArrayItem("JDSUCisco")]
        public JDSUCiscoClass[] JDSUCiscoArray;
        [XmlArray("Cisco")]
        public IPCom[] CiscoList;

        public forSerialize()
        { }
    }
}
