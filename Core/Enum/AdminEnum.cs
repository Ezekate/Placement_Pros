using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
   public enum AdminEnum
    {
        [Description("Gender")]
        Gender = 1,
        [Description("Country")]
        Country,
     

    }
    public enum JobType
    {
        [Description("For Remote")]
        Remote,
        [Description("for Onsite")]
        Onsite,
    }
}
