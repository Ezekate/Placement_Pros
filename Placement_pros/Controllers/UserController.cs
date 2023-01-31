using Core.Database;
using Core.Models;
using Core.VeiwModel;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Placement_pros.Controllers
{
    public class UserController : BaseController
    {
        private readonly IDropdownHelper _dropdownHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IUserHelper _userHelper;
        public UserController(IDropdownHelper dropdownHelper, UserManager<ApplicationUser> userManager, AppDbContext dbContext, IUserHelper userHelper = null)
        {
            _dropdownHelper = dropdownHelper;
            _userManager = userManager;
            _dbContext = dbContext;
            _userHelper = userHelper;
        }
        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                ViewBag.Gender = _dropdownHelper.GetGenderDropdown();
                ViewBag.Countries = _dropdownHelper.GetCountryDropdown();

                //To get the userName of current login user to get their details from database
                var userName = User?.Identity?.Name;
                if (userName == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //calling method from userhelper
                var profile = _userHelper.GetUserProfile(userName).Result;
                //var getProfile = _userHelper.GetProfilelList();
                return View(profile);

            }

            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Skill(SkillViewModel skill)
        {
            try
            {
                var userName = User?.Identity?.Name;
                var user = _userManager.FindByNameAsync(userName).Result;
                if (user == null)
                {

                    return RedirectToAction("Profile", "User");
                }
                var createSkill = _userHelper.CreateSkill(skill, user.Id);
                if (createSkill != null)
                {
                    return RedirectToAction("Profile", "User");

                }
                return RedirectToAction("Profile", "User");
            }
            catch (Exception)
            {

                throw;
            }
        }
        //[HttpPost]
        public IActionResult EducationalQualification(EducationalQualificationVeiwModel education)
        {
            try
            {
                var userName = User?.Identity?.Name;
                var user = _userManager.FindByNameAsync(userName).Result;
                if (user == null)
                {
                    return RedirectToAction("Profile", "User");
                }
                var createEducation = _userHelper.CreateEducationQualification(education, user.Id);
                if (createEducation != null)
                {
                    return RedirectToAction("Profile", "User");

                }
                return RedirectToAction("Profile", "User");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult WorkExperience(WorkExperienceViewModel workExperience)
        {
            try
            {
                var userName = User?.Identity?.Name;
                var user = _userManager.FindByNameAsync(userName).Result;
                if (user == null)
                {

                    return RedirectToAction("Profile", "User");

                }
                var createworkExperience = _userHelper.CreateWorkExperience(workExperience, user.Id);
                if (createworkExperience != null)
                {
                    return RedirectToAction("Profile", "User");

                }
                return RedirectToAction("Profile", "User");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public JsonResult EditPersonalInfo(PersonalInfoViewModel personalInfo)
        {
            try
            {
                ViewBag.Gender = _dropdownHelper.GetGenderDropdown();
                ViewBag.Countries = _dropdownHelper.GetCountryDropdown();

                var userName = User?.Identity?.Name;
                //// var userToBeEdited = _userManager.FindByNameAsync(userName).Result;
                if (userName == null)
                {
                    return Json(new { isError = true, data = "" });
                }
                personalInfo.UserName = userName;
                if (string.IsNullOrEmpty(personalInfo.FirstName))
                {
                    return Json(new { isError = true, data = "" });
                }
                var useEdited = _userHelper.EditPersonalInfo(personalInfo);

                if (useEdited == true)
                {
                    return Json(new { isError = false, msg = "user detail updated successfully " });
                }
                return Json(new { isError = true, data = "" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Jobs()
        {
            try
            {
                ViewBag.dropdown = _dropdownHelper.GetJobTypeDropdown();
                //var job = _dbContext.Jobs.ToList();
                var job = _userHelper.AvaliableJobs();
                return View(job);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult JobApplication(Guid id)
        {
            try
            {
                var get = _userHelper.GetDescription(id);
                return View(get);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public JsonResult JobApplication(string jobApplication)
        {
            try
            {
                //to find the person that User
                var username = User.Identity.Name;
                //checking for null
                if (jobApplication != null && username != null)
                {
                    var jobData = JsonConvert.DeserializeObject<JobApplications>(jobApplication);
                    var userId = _dbContext.Users.Where(a => a.UserName == username).FirstOrDefault().Id;
                    jobData.UserId = userId;
                    var job = _userHelper.CreatResume(jobData);
                    if (job != null)
                    {
                        return Json(new { isError = false, msg = "Application Submitted successful" });
                    }
                }

                return Json(new { isError = true, msg = "Application Failed" });

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult JobSearch(string searchData)
        {
            try
            {
                //checking for null
                if (searchData != null)
                {
                    var searchModel = JsonConvert.DeserializeObject<JobVeiwModel>(searchData);
                    if (searchModel != null)
                    {
                        var job = _userHelper.JobFilter(searchModel);
                        if (job.Any())
                        {
                            return Json(new { isError = false, data = job });
                        }
                    }
                }

                return Json(new { isError = true, msg = " Failed to Fetch data" });

            }
            catch (Exception)
            {

                throw;
            }

        }


        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserDashBoard( string userid)
        {
            
                var jobsCount = _userHelper.GetJobsbyId(userid).Count;
               var jobVeiw = new JobApplicationVIewModel()
               {
                        Jobcount = jobsCount,     
              };
                
                return View(jobVeiw);


        }


    }
}
