using Core.Database;
using Core.Enum;
using Logic.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Helpers
{
    public class DropdownHelper :IDropdownHelper
    {
        private readonly AppDbContext _dbContext;

        public DropdownHelper(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<DropdownEnumModel> GetDropdown()
        {
            var common = new DropdownEnumModel()
            {
                Id = 0,
                Name = "-- Select --"

            };
            var enumList = ((DropdownEnum[])Enum.GetValues(typeof(DropdownEnum))).Select(c => new DropdownEnumModel() { Id = (int)c, Name = c.ToString() }).ToList();
            enumList.Insert(0, common);
            return enumList;
        }
        //method to getdropdown
        public List<DropdownEnumModel> GetGenderDropdown()
        {
            var common = new DropdownEnumModel()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var gender = _dbContext.Gender.Where(y => y.Deleted == false).Select(a => new DropdownEnumModel() {Id = a.Id, Name = a.Name }).ToList();
            gender.Insert(0, common);
            return gender;
        }
        public List <DropdownEnumModel> GetCountryDropdown()
        {
            var common = new DropdownEnumModel()
        {
            Id = 0,
                Name = "-- Select --"

            };
            var country = _dbContext.Country.Where(y => y.Deleted == false).Select(a => new DropdownEnumModel() { Id = a.Id, Name = a.Name }).ToList();
            country.Insert(0, common);
            return country;

        }
        public List<DropdownEnumModel> GetJobTypeDropdown()
        {
            var newjob = new DropdownEnumModel()
            {

                Id = 0,
                Name = "-- Select --"
            };
            var job = ((JobTypeEnum[])Enum.GetValues(typeof(JobTypeEnum))).Select(c => new DropdownEnumModel() { Id = (int)c, Name = c.ToString() }).ToList();
            job.Insert(0, newjob);
            return job;
        }
       

        public class DropdownEnumModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
       
    }
}
