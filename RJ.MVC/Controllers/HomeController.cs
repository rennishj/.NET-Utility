using RJ.Configuration;
using System.Web.Mvc;

namespace RJ.MVC.Controllers {
    public class HomeController : Controller
    {
        private readonly IConfigurationSource _configSource;

        public HomeController(IConfigurationSource configSource) {
            _configSource = configSource;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = _configSource.GetValue("misc:helloMessage");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}