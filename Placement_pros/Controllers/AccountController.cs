
using Core.Database;
using Core.Models;
using Core.VeiwModel;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Placement_pros.Controllers
{//constructors
    public class AccountController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDropdownHelper _dropdownHelper;
        private readonly IUserHelper _userHelper;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(AppDbContext dbContext, IDropdownHelper dropdownHelper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserHelper userHelper)
        {
            _dbContext = dbContext;
            _dropdownHelper = dropdownHelper;
            _userManager = userManager;
            _userHelper = userHelper;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View(); 
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVeiwModel loginVeiwModel)
        {
            try
            {               
                if (string.IsNullOrEmpty(loginVeiwModel.Email))
                {
                    SetMessage("invalid Email", Message.Category.Error);
                    return View(loginVeiwModel);
                    
                }               
                if (string.IsNullOrEmpty(loginVeiwModel.Password))
                {
                    SetMessage("invalid password", Message.Category.Error);
                    return View(loginVeiwModel);
                }
                var validUser = await _userManager.FindByEmailAsync(loginVeiwModel.Email);
                if (validUser == null)
                {
                    SetMessage(" Email input doesn't belong to any user", Message.Category.Error);
                    return View(loginVeiwModel);
                }
                var signIn = await _signInManager.PasswordSignInAsync(validUser, loginVeiwModel.Password, true, false);
                
                if (signIn.Succeeded)
                {
                  var user = User.Identity.Name;
                    return RedirectToAction("Index", "Home");
                }
                SetMessage(" Login failed", Message.Category.Error);              
                return View(loginVeiwModel);           
            }           
            catch (Exception)
            {             

                throw;
            }
        }
        [HttpGet]
        public IActionResult LoadState(int id)
        {
            var state = _dbContext.State.Where(x => x.CountryId == id).ToList();
            return Json(state);
        }
          [HttpGet]
        public IActionResult RegisterAdmin()
        {
            try
            {
                ViewBag.Gender = _dropdownHelper.GetGenderDropdown();
                ViewBag.Countries = _dbContext.Country.Where(s => s.Id != 0).ToList();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(ApplicationUserViewModel model)
        {
            try
            {
                ViewBag.Gender = _dropdownHelper.GetGenderDropdown();
                ViewBag.Countries = _dbContext.Country.Where(s => s.Id != 0).ToList();

                if (model != null)
                {
                    if (model.Password != model.ConfirmPassword)
                    {
                        SetMessage("Password and ComfirmPassword doesn't match", Message.Category.Error);
                        return View(model);
                    }
                    var validUser = await _userManager.FindByEmailAsync(model.Email);

                    if (validUser != null)
                    {
                        SetMessage(" Email  belong to another user", Message.Category.Error);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.FirstName))
                    {
                        SetMessage(" FirstName can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.LastName))
                    {
                        SetMessage(" LastName can not be empty", Message.Category.Error);
                        return View(model);
                    }

                    if (model.GenderId <= 0)
                    {

                        SetMessage(" Gender can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    if (model.StateId <= 0)
                    {

                        SetMessage(" State can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    if (model.CountryId <= 0)
                    {
                        SetMessage(" Country can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    var userDetails = await _userHelper.CreateAdmin(model);
                    if (userDetails != null)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
                SetMessage("Invalide Login Attempt", Message.Category.Error);
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Register() 
        {
            try
            {
                ViewBag.Gender = _dropdownHelper.GetGenderDropdown();
                ViewBag.Countries = _dbContext.Country.Where(s => s.Id != 0).ToList();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUserViewModel model)
        {
            try
            {
                ViewBag.Gender = _dropdownHelper.GetGenderDropdown();
                ViewBag.Countries = _dbContext.Country.Where(s => s.Id != 0).ToList();

                if (model != null)
                {
                    if (model.Password != model.ConfirmPassword)
                    {
                        SetMessage("Password and ComfirmPassword doesn't match", Message.Category.Error);
                        return View(model);
                    }
                    var validUser = await _userManager.FindByEmailAsync(model.Email);

                    if (validUser != null)
                    {
                        SetMessage(" Email  belong to another user", Message.Category.Error);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.FirstName))
                    {
                        SetMessage(" FirstName can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.LastName))
                    {
                        SetMessage(" LastName can not be empty", Message.Category.Error);
                        return View(model);
                    }

                    if (model.GenderId  <=  0) 
                    {

                        SetMessage(" Gender can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    if (model.StateId  <= 0)
                    {

                        SetMessage(" State can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    if (model.CountryId <= 0)
                    {
                        SetMessage(" Country can not be empty", Message.Category.Error);
                        return View(model);
                    }
                    var userDetails = await _userHelper.CreateUser(model);
                    if (userDetails != null)
                    {
                        return RedirectToAction("Login","Account");
                    }
                }
                SetMessage("Invalide Login Attempt", Message.Category.Error);
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
      

        public async Task <IActionResult>LogOut() 
        {
            await _signInManager.SignOutAsync();
             return RedirectToAction("Index","Home");
           
        }

    }
}
