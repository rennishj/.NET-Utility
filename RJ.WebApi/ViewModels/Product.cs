using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJ.WebApi.ViewModels
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}