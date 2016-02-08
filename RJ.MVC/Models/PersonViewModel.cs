using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJ.MVC.Models
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressViewModel Address { get; set; }

    }

    public class AddressViewModel
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}