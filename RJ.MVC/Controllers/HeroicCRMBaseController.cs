using RJ.MVC.CustomActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RJ.MVC.Controllers
{
    public class HeroicCRMBaseController : Controller
    {
        public BetterJsonResult<T> BetterJson<T>(T model)
        {
            return new BetterJsonResult<T>() { Data = model };
        }
    }
}