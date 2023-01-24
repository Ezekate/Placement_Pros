using System;
using System.Collections.Generic;
using System.Text;
using static Logic.Helpers.DropdownHelper;

namespace Logic.IHelpers
{
    public interface IDropdownHelper
    {
        List<DropdownEnumModel> GetDropdown();
        List<DropdownEnumModel> GetCountryDropdown();
        List<DropdownEnumModel> GetGenderDropdown();
        List<DropdownEnumModel> GetJobTypeDropdown();


    }
}
