using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RJ.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [Required(ErrorMessage="Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        public DateTime? ReleaseDate { get; set; }
    }
}