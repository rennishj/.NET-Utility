using RJ.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RJ.WebApi.Controllers
{
    [EnableCorsAttribute("http://localhost:27178", "*", "*")]
    public class ProductsController : ApiController
    {
        [Route("api/products/all")]
        public IHttpActionResult Get()
        {
            return Ok(ProductsReadAll());
        }

        [Route("api/products")]
        public IHttpActionResult Search(string search)
        {
            return Ok(ProductsReadAll().Where(p => p.ProductCode.Contains(search)).ToList());
        }

        [Route("api/product/{search}")]
        public IHttpActionResult Get(string search)
        {
            var result = ProductsReadAll().Where(p => p.ProductCode.Contains(search)).ToList();
            return Ok(result);
        }
        
        [Route("api/product/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(ProductsReadAll().FirstOrDefault(p => p.ProductId == id));
        }
        
        [Route("api/product/{id:int}")]
        public IHttpActionResult Put(int id,[FromBody]Product product)
        {
            return Ok();
        }

        [Route("api/product")]
        public IHttpActionResult Post([FromBody]Product product)
        {
            return Ok(new { Message = "Product created successfully" });
        }

        private List<Product> ProductsReadAll()
        {
            var products = new List<Product>()
            {
                new Product{ProductId = 1,ProductName = "Professional WCF4",Description = "Intro to WCF4",Price = 23.45m,ProductCode = "IWCF",ReleaseDate = new DateTime(2015,02,28)},
                new Product{ProductId = 2,ProductName = "Asp.Net MC5" ,Description = "Intro to MVC5",Price = 23.45m,ProductCode = "MVC4",ReleaseDate = new DateTime(2015,03,28)},
                new Product{ProductId = 3 ,ProductName = "Inside MS SQL Server" ,Description = "Intro to SQL",Price = 23.45m,ProductCode = "WebAPI2",ReleaseDate = new DateTime(2015,04,28)},
                new Product{ProductId = 4,ProductName = "Javacsript Primer" ,Description = "Intro to JAVACSRIPT",Price = 23.45m,ProductCode = "C#",ReleaseDate = new DateTime(2015,05,28)},
                new Product{ProductId = 5,ProductName = "Responsive Design with BS" ,Description = "Intro to Bootstrap",Price = 23.45m,ProductCode = "VB.NET",ReleaseDate = new DateTime(2015,06,28)},
                new Product{ProductId = 6,ProductName = "Professional AngularJs" ,Description = "Intro to AngularJs",Price = 23.45m,ProductCode = "JS",ReleaseDate = new DateTime(2015,07,28)}
            };
            return products;
        }
    }
}
