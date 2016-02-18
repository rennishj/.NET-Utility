using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RJ.Utils;
using RJ.Poco;
using System.IO;
using RJ.MVC.Models;

namespace RJ.MVC.Controllers
{
    public class ExcelController : Controller
    {      
        public ActionResult Export()
        {
            try
            {
                var data = ExcelHelper.CreateExcel<Person>(GetAllPersons()).GetAsByteArray();
                var vm = new ExcelViewModel
                {                   
                    Data = data,
                    FileName = string.Format("ExcelDownLoad{0:MMddyy_hhmmss}.xlsx",DateTime.Now)
                };
                return File(vm.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", vm.FileName);
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment; filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(((MemoryStream)stream).ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            
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