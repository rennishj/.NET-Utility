using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RJ.Utils;
using RJ.Poco;
using System.IO;

namespace RJ.MVC.Controllers
{
    public class ExcelController : Controller
    {      
        public ActionResult Export()
        {
            try
            {
                var stream = ExcelHelper.CreateExcelPackage(GetAllPersons());                
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=ExcelDemo.xlsx");
                Response.BinaryWrite(((MemoryStream)stream).ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return View();
        }

        private List<Person> GetAllPersons()
        {
            return new List<Person>()
            {            
                 new Person{PersonId = 1,FirstName  = "Rennish",LastName = "Joseph",Email = "rennishj@gmail.com",ZipCode = "32225"},
                 new Person{PersonId = 2,FirstName  = "Rennish",LastName = "Joseph",Email = "rennishj@gmail.com",ZipCode = "32225"},
                 new Person{PersonId = 3,FirstName  = "Rennish",LastName = "Joseph",Email = "rennishj@gmail.com",ZipCode = "32225"},
                 new Person{PersonId = 4,FirstName  = "Rennish",LastName = "Joseph",Email = "rennishj@gmail.com",ZipCode = "32225"},
                 new Person{PersonId = 5,FirstName  = "Rennish",LastName = "Joseph",Email = "rennishj@gmail.com",ZipCode = "32225"}
            };
        }
    }
}