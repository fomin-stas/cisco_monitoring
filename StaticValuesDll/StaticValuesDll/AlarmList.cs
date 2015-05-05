using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace StaticValuesDll
{
    [DataContract]
  public class AlarmList
  {
      [DataMember]
      public Int64 Id { get; set; }

      [DataMember]
      public String Name { get; set; }

      [DataMember]
      public int Execute { get; set; }


    }
}
