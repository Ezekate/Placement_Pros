using Core.Database;
using Core.Models;
using Core.VeiwModel;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{

    public class UserHelpers : IUserHelper
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserHelpers(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        //mapping from applicationViewModel into applicationuser
        public async Task<ApplicationUser> CreateUser(ApplicationUserViewModel applicationUserViewModel)
        {
            //mapping from applicationViewModel into applicationuser
            var applicationUser = new ApplicationUser
            {
                FirstName = applicationUserViewModel.FirstName,
                LastName = applicationUserViewModel.LastName,
                Email = applicationUserViewModel.Email,
                GenderId = applicationUserViewModel.GenderId,
                CountryId = applicationUserViewModel.CountryId,
                StateId = applicationUserViewModel.StateId,
                DateCreated = DateTime.Now,
                UserName = applicationUserViewModel.Email,
            };
            var result = await _userManager.CreateAsync(applicationUser, applicationUserViewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "User");
                return applicationUser;
            }
        
            return null;
        }
        public async Task<ApplicationUser> CreateAdmin(ApplicationUserViewModel applicationUserViewModel)
        {
            //mapping from applicationViewModel into applicationuser
            var applicationUser = new ApplicationUser
            {
                FirstName = applicationUserViewModel.FirstName,
                LastName = applicationUserViewModel.LastName,
                Email = applicationUserViewModel.Email,
                GenderId = applicationUserViewModel.GenderId,
                CountryId = applicationUserViewModel.CountryId,
                StateId = applicationUserViewModel.StateId,
                DateCreated = DateTime.Now,
                UserName = applicationUserViewModel.Email,
            };
            var result = await _userManager.CreateAsync(applicationUser, applicationUserViewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "Admin");
                return applicationUser;
            }
            return null;
        }
        public async Task<ProfileVeiwModel> GetUserProfile(string userName)
        {
            //creating a new instance of profileVeiwModel into profileDetails
            var profileDetails = new ProfileVeiwModel();
            //querying the database
            var user = _userManager.Users.Where(x => x.UserName == userName).Include(a => a.Gender).Include(d => d.Country).Include(c => c.State).FirstOrDefault();

            if (user != null)
            {

                var profileInfo = new PersonalInfoViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    ProfilePicture = user.ProfilePicture,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Gender = user.Gender?.Name,
                    GendeId = user.GenderId.Value,
                    CountryId = user.CountryId.Value,
                    Country = user.Country?.Name,
                    State = user.State?.Name,
                    Birthdate = user.BirthDate,
                    StateId = user.StateId == null ? 0 : user.StateId.Value,

                };

                profileDetails.PersonalInfo = profileInfo;
            }

            //a check  if user is null

            //UserManager only works for modal that contains identity

            var userSkill = _dbContext.Skills.Where(x => x.UserId == user.Id)?.ToList()?
                .Select(s => new SkillViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                }
                ).ToList();
            if (userSkill.Any())
            {
                profileDetails.Skills = userSkill;
            }
            //querying the database
            var userWork = _dbContext.WorkExperiences.Where(r => r.UserId == user.Id)?.ToList()?
                .Select(y => new WorkExperienceViewModel
                {
                    Id = y.Id,
                    Location = y.Location,
                    WorkPlace = y.WorkPlace,
                    Discription = y.Discription,
                    DateStarted = y.DateClosed,
                    DateAdded = y.DateAdded,
                }
                ).ToList();
            if (userWork.Any())
            {
                profileDetails.WorkExperiences = userWork;
            }
            var userEducation = _dbContext.EducationalQualifications.Where(x => x.UserId == user.Id).ToList()
             .Select(s => new EducationalQualificationVeiwModel
             {
                 Id = s.Id,
                 Name = s.Name,
                 FieldOfStudy = s.FieldOfStudy,
                 Grade = s.Grade,
                 Degree = s.Degree,
                 StartDate = s.StartDate,
                 EndDate = s.EndDate,
             }
             ).ToList();
            if (userEducation.Any())
            {
                profileDetails.EducationalQualifications = userEducation;
            }
            return profileDetails;
        }


        public async Task<Skill> CreateSkill(SkillViewModel SkillViewModel, string userId)
        {

            var newskill = _dbContext.Skills.Where(x => x.Name == SkillViewModel.Name && x.UserId == userId).FirstOrDefault();
            if (newskill == null)
            {
                var skill = new Skill
                {
                    Id = SkillViewModel.Id,
                    Name = SkillViewModel.Name,
                    Active = true,
                    Deleted = false,
                    UserId = userId
                };
                _dbContext.Add(skill);
                _dbContext.SaveChanges();
                return skill;
            }
            return null;
        }

        public async Task<EducationalQualification> CreateEducationQualification(EducationalQualificationVeiwModel qualification, string userId)
        {
            var newQualification = _dbContext.EducationalQualifications.Where(y => y.Name == qualification.Name && y.UserId == userId).FirstOrDefault();
            if (newQualification == null)
            {
                var education = new EducationalQualification
                {
                    Id = qualification.Id,
                    Name = qualification.Name,
                    Degree = qualification.Degree,
                    Grade = qualification.Grade,
                    FieldOfStudy = qualification.FieldOfStudy,
                    StartDate = qualification.StartDate,
                    EndDate = qualification.EndDate,
                    Deleted = false,
                    Active = true,
                    UserId = userId,
                };
                _dbContext.Add(education);
                _dbContext.SaveChanges();
                return education;
            }
            return null;
        }
        public async Task<WorkExperience> CreateWorkExperience(WorkExperienceViewModel WorkExperience, string userId)
        {
            var newWork = _dbContext.WorkExperiences.Where(y => y.UserId == userId).FirstOrDefault();
            if (newWork == null)
            {
                var work = new WorkExperience
                {
                    Id = WorkExperience.Id,
                    Location = WorkExperience.Location,
                    Discription = WorkExperience.Discription,
                    DateAdded = DateTime.Now,
                    DateStarted = WorkExperience.DateStarted,
                    DateClosed = WorkExperience.DateClosed,
                    WorkPlace = WorkExperience.WorkPlace,
                    UserId = userId,
                    Deleted = false,
                    Active = true,
                };
                _dbContext.Add(work);
                _dbContext.SaveChanges();
                return work;
            }
            return null;
        }

        public bool EditSKill(SkillViewModel skill)
        {
            var skillTobeEdit = _dbContext.Skills.Where(x => x.Name == skill.Name).FirstOrDefault();

            if (skillTobeEdit != null)
            {
                skillTobeEdit.Name = skill.Name;


                _dbContext.Update(skillTobeEdit);
                _dbContext.SaveChanges();

                return true;
            }

            return true;
        }


        public bool EditPersonalInfo(PersonalInfoViewModel personalInfo)
        {
            var userToEdit = _userManager.Users.Where(x => x.UserName == personalInfo.UserName).FirstOrDefault();

            if (userToEdit != null)
            {

                userToEdit.FirstName = personalInfo.FirstName;
                userToEdit.LastName = personalInfo.LastName;
                userToEdit.Email = personalInfo.Email;
                userToEdit.PhoneNumber = personalInfo.PhoneNumber;
                userToEdit.Address = personalInfo.Address;
                userToEdit.ProfilePicture = personalInfo.ProfilePicture == null ? userToEdit.ProfilePicture : personalInfo.ProfilePicture;
                userToEdit.BirthDate = personalInfo.Birthdate == null ? userToEdit.BirthDate : personalInfo.Birthdate;


                _dbContext.Update(userToEdit);
                _dbContext.SaveChanges();

                return true;
            }
            _dbContext.Update(userToEdit);
            _dbContext.SaveChanges();

            return true;
        }

        public List<JobVeiwModel> AvaliableJobs()
        {
            var ListJob = new List<JobVeiwModel>();
            var job = _dbContext.Jobs.Where(r => r.Active == true)?.ToList();
            var jobs = job.Select(z => new JobVeiwModel()

            {
                Id = z.Id,
                Location = z.Location,
                Discription = z.Discription,
                CompanyName = z.CompanyName,
                Salary = z.Salary,
                Title = z.Title,
                JobType = z.JobType,

            }).ToList();
            if (jobs.Any())
            {
                return jobs;
            }
            return ListJob;
        }

        public JobVeiwModel GetDescription(Guid id)
        {
            var getnew = new JobVeiwModel();
            if (id != Guid.Empty)
            {
                var get = _dbContext.Jobs.Where(r => r.Id == id).FirstOrDefault();
                if (get != null)
                {
                    var myJob = new JobVeiwModel()
                    {
                        Id = get.Id,
                        Discription = get.Discription,
                        Requirement = get.Requirement,
                        CompanyName = get.CompanyName,
                        Location = get.Location,
                        Title = get.Title,
                        Salary = get.Salary,
                        JobType = get.JobType
                    };
                    return myJob;
                }
            }
            return getnew;
        }
        // fetching descrition and requirement
        public JobApplications CreatResume(JobApplications jobapplication)
        {
            if (jobapplication != null)
            {
                _dbContext.Add(jobapplication);
                _dbContext.SaveChanges();

                return jobapplication;
            }
            return null;
        }
        //job Filter
        public List<JobVeiwModel> JobFilter(JobVeiwModel jobsearch)
        {
            var filter = new List<Job>();
            var searchResult = new List<JobVeiwModel>();
            if (jobsearch != null)
            {
                var allJobs = _dbContext.Jobs.Where(r => r.Id != Guid.Empty && r.Active).ToList();
                if (!string.IsNullOrEmpty(jobsearch.CompanyName))
                {
                    //searching forany name that contain the content
                    var searchItem = jobsearch.CompanyName.ToLower();
                    var results = allJobs.Where(r => r.CompanyName.ToLower().Contains(searchItem)).ToList();

                    filter.AddRange(results);

                }
                if (!string.IsNullOrEmpty(jobsearch.Location))
                {

                    var results = allJobs.Where(r => r.Location.ToLower().Contains(jobsearch.Location.ToLower())).ToList();
                    filter.AddRange(results);
                }
                if (!string.IsNullOrEmpty(jobsearch.Title))
                {
                    var results = allJobs.Where(r => r.Title.ToLower().Contains(jobsearch.Title.ToLower())).ToList();
                    filter.AddRange(results);
                }
                searchResult = filter.Select(z => new JobVeiwModel()
                {
                    Id = z.Id,
                    Location = z.Location,
                    Discription = z.Discription,
                    CompanyName = z.CompanyName,
                    Salary = z.Salary,
                    Title = z.Title,
                    Type = z.JobType.ToString(),

                }).ToList();
                return searchResult.GroupBy(x => x.Id).Select(r => r.First()).ToList();
            }
            return null;
        }
        public List<JobApplications>GetJobsbyId( string userName)
        {
            var userid = _dbContext.Users.Where(x => x.UserName == userName).FirstOrDefault().Id;
            var getalljob = new List<JobApplications>();
            if (getalljob != null)
            {
                var allJobs = _dbContext.JobApplications.Where(v => v.UserId == userid).ToList();
                return allJobs;
            }
            return getalljob;


        }
    
    }
}
