using RJ.MVC.Models;
using System.Web.Mvc;
using RJ.MVC.Extensions;

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
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            return null;
        }
    }
}