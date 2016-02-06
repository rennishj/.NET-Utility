using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RJ.MVC.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return View();
        }
    }
}