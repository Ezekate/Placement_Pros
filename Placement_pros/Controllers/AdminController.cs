using Core.Database;
using Core.Models;
using Core.VeiwModel;
using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Placement_pros.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminHelper _adminHelper;
        private readonly IDropdownHelper _dropdownHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;


        public AdminController(IAdminHelper adminHelper, IDropdownHelper dropdownHelper, UserManager<ApplicationUser> userManager, AppDbContext dbContext)
        {
            _adminHelper = adminHelper;
            _dropdownHelper = dropdownHelper;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult CreateDropdown()
        {
            try
            {
                ViewBag.dropdown = _dropdownHelper.GetDropdown();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult CreateDropdown(CreateDropDownViewModel createDropDown)
        {
            ViewBag.dropdown = _dropdownHelper.GetDropdown();
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(createDropDown.Name))
                    {
                        return View(createDropDown);
                    }
                    if (createDropDown.DropdownId <= 0)
                        return View(createDropDown);

                    var result = _adminHelper.CreateDropdown(createDropDown);
                    if (result == true)
                    {
                        ModelState.Clear();
                        return View();
                    }
                }
                return View(createDropDown);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult CreateJob()
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
        [HttpPost]
        public IActionResult CreateJob(JobVeiwModel jobUpload)
        {
            ViewBag.dropdown = _dropdownHelper.GetJobTypeDropdown();
            try
            {
                if (string.IsNullOrEmpty(jobUpload.CompanyName))
                {
                    SetMessage("CompanyName is empty", Message.Category.Error);
                    return View(jobUpload);
                }

                if (string.IsNullOrEmpty(jobUpload.Location))
                {
                    SetMessage("Location is empty", Message.Category.Error);
                    return View(jobUpload);
                }

                if (jobUpload.JobType > 0)
                {
                    SetMessage("JobType is empty", Message.Category.Error);
                    return View(jobUpload);
                }

                if (string.IsNullOrEmpty(jobUpload.Salary))
                {
                    SetMessage("Salary is empty", Message.Category.Error);
                    return View(jobUpload);
                }
                var CreateJob = _adminHelper.CreateJob(jobUpload);
                if (CreateJob != null)
                {
                    ModelState.Clear();

                    SetMessage("There is no available job", Message.Category.Error);
                    return View();
                }
                return RedirectToAction("ListOfJob", "Admin");
            }

            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult AdminJob()
        {
            var veiwjob = _adminHelper.GetJobs();
            return View(veiwjob);
        }
        public JsonResult Delist( Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { isError = true, data = "Failed to de list job" });

            }
            var result= _adminHelper.DeList(id);
            if (result == true)
            {
                return Json(new { isError = false, msg = "Job de-listed successfully " });
            }
            return Json(new { isError = true, data = "Failed to de list job" });
        }

        [HttpGet]
        public IActionResult AdminJobApplication()
        {
            try
            {

                var getjob = _adminHelper.GetApplicationJobs();
                return View(getjob);


            }
            
            catch (Exception)
            {

                throw;
            }
        }
    }
}
