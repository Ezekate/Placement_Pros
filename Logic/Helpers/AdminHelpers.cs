using Core.Database;
using Core.Enum;
using Core.Models;
using Core.VeiwModel;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logic.Helpers.DropdownHelper;

namespace Logic.Helpers
{
    public class AdminHelpers : IAdminHelper
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AdminHelpers(AppDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public bool CreateDropdown(CreateDropDownViewModel dropdown)
        {
            var enumName = ((DropdownEnum[])Enum.GetValues(typeof(DropdownEnum))).Select(c => new DropdownEnumModel() { Id = (int)c, Name = c.ToString() })
                   .Where(a => a.Id == dropdown.DropdownId).FirstOrDefault();

            if (enumName.Name == "Gender")
            {
                var checkName = _dbContext.Gender.Where(a => a.Name == dropdown.Name).FirstOrDefault();
                if (checkName != null)
                {
                    return false;
                }
                //this is where we are mapping the CreateDropDownViewModel into the database

                var gender = new Gender
                {
                    Name = dropdown.Name,
                    Active = true,
                    Deleted = false,
                };
                _dbContext.Add(gender);
                _dbContext.SaveChanges();

                return true;
            }
            if (enumName.Name == "Country")
            {
                var checkName = _dbContext.Country.Where(a => a.Name == dropdown.Name).FirstOrDefault();
                if (checkName != null)
                {
                    return false;
                }
                var country = new Country
                {
                    Name = dropdown.Name,
                    Active = true,
                    Deleted = false,

                };
                _dbContext.Add(country);
                _dbContext.SaveChanges();
                return true;

            }
            return false;
        }

        public async Task<Job> CreateJob(JobVeiwModel jobload)
        {

            if (jobload != null)
            {
                var createjob = new Job
                {
                    CompanyName = jobload.CompanyName,
                    Location = jobload.Location,
                    Title = jobload.Title,
                    Discription = jobload.Discription,
                    Requirement = jobload.Requirement,
                    CompanyLogo = jobload.CompanyLogo,
                    Salary = jobload.Salary,
                    JobType = jobload.JobType,
                    Active = true,
                };
                _dbContext.Add(createjob);
                _dbContext.SaveChanges();
                return createjob;
            }
            return null;
        }


        public List<JobVeiwModel> GetJobs()
        {
            var ListJob = new List<JobVeiwModel>();
            var job = _dbContext.Jobs.Where(r => r.Id != Guid.Empty)?.ToList();
            var jobs = job.Select(z => new JobVeiwModel()
            {
                Id = z.Id,
                Location = z.Location,
                CompanyName = z.CompanyName,
                Salary = z.Salary,
                Title = z.Title,
                JobType = z.JobType,
                Active = z.Active,
            }).ToList();
            if (jobs.Any())
            {
                return jobs;
            }
            return ListJob;
        }
        public bool DeList(Guid id)
        {
            try
            {
                var jobToEdit = _dbContext.Jobs.Where(v =>v.Id == id).FirstOrDefault();
                if (jobToEdit==null)
                {
                    return false;
                }
                jobToEdit.Active = false;

                _dbContext.Update(jobToEdit);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
   
}

