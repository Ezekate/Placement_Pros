using Core.Database;
using Core.Models;
using Core.VeiwModel;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                    return RedirectToAction( "Login","Account");
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
                    return View();

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
                    return View();

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
                    return View();

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
                    return Json(new {isError = true, data = ""});
                }
                personalInfo.UserName = userName;
                if(string.IsNullOrEmpty(personalInfo.FirstName))
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
        public IActionResult Contact()
        {
            try
            {
                ViewBag.dropdown = _dropdownHelper.GetJobTypeDropdown();
              
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }


}
