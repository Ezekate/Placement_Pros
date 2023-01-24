using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Placement_pros.Controllers
{
    public class BaseController : Controller
    {
        protected void SetMessage(string message, Message.Category messageType)
        {
            Message msg = new Message(message, (int)messageType);
            TempData["Message"] = msg;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
