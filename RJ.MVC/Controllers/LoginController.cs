using RJ.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RJ.MVC.Controllers
{
    public class LoginController : Controller
    {        
        public ActionResult Login()
        {
            LoginViewModel login = new LoginViewModel();
            return View(login);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(new { Id = 1, Uname = "rennishj@gmail.com", Password = "123456" });
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}