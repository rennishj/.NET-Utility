using System;
using System.Collections.Generic;

namespace RJ.Poco
{
    //public class NetUtility
    //{
    //    public Order Order { get; set; }
    //}
   public class Order
    {
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public List<Package> Packages { get; set; }
    }
   public class Customer

   {
       public int CustomerId { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Email { get; set; }
   }

   public class Address
   {
       public string Address1 { get; set; }
       public string Zip { get; set; }
       public string State { get; set; }
       public string Country { get; set; }
   }

   public class Package
   {
       public int PackageId { get; set; }
       public int OrderId { get; set; }
       public DateTime ShipDate { get; set; }
       public List<PackageItems> PackageItems { get; set; }

   }
   public class PackageItems
   {
       public int PackageItemId { get; set; }
       public int PackageId { get; set; }
       public int Qty { get; set; }
       public string Size { get; set; }
   }
}
