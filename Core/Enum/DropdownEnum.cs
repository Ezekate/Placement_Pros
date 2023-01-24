using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
    public enum DropdownEnum
    {
        [Description("Gender")]
        Gender = 1,
        [Description("Country")]
        Country,
  

    }

    public enum JobTypeEnum
    {
        [Description("For Remote")]
        Remote = 1,
        [Description("For Onsite")]
        Onsite,
    }
}
