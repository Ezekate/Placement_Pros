using Core.Models;
using Core.VeiwModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
        //method called from userHelper
        Task<ApplicationUser> CreateUser(ApplicationUserViewModel applicationUserViewModel);
        Task<ProfileVeiwModel> GetUserProfile(string userName);
        //List<PersonalInfoViewModel> GetProfilelList(ApplicationUser UserName);
        Task<Skill> CreateSkill(SkillViewModel SkillViewModel, string userId);
        Task<EducationalQualification> CreateEducationQualification(EducationalQualificationVeiwModel qualification, string userId);
        Task<WorkExperience> CreateWorkExperience(WorkExperienceViewModel workExperience, string userId);
        bool EditPersonalInfo(PersonalInfoViewModel personalInfo);
        List<JobVeiwModel> AvaliableJobs();
        JobVeiwModel GetDescription(Guid id);
        JobApplications CreatResume(JobApplications jobapplication);
        List<JobVeiwModel> JobFilter(JobVeiwModel jobsearch);
        List<JobApplications> GetJobsbyId(string userName);
        Task<ApplicationUser> CreateAdmin(ApplicationUserViewModel applicationUserViewModel);
        bool EditSKill(SkillViewModel skill);
        bool EditEducation(EducationalQualificationVeiwModel education);
        bool EditWork(WorkExperienceViewModel work);
    }
}

