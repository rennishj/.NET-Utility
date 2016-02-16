using RJ.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace RJ.MVC.Controllers
{
    public class ContactUsController : Controller
    {       
        [HttpGet]        
        public ActionResult ContactUs()
        {
            var contactUs = new ContactUsViewModel();
            return PartialView(contactUs);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            { 
                
            }
            return null;
        }
    }
}