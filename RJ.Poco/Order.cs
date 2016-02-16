using System;
using System.Collections.Generic;

namespace RJ.Poco
{
   public class Order
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public List<Package> Packages { get; set; }
    }

   public class Package
   {
       public int PackageId { get; set; }
       public int OrderId { get; set; }
       public DateTime ShipDate { get; set; }
       public List<PackageItems> Items { get; set; }

   }
   public class PackageItems
   {
       public int PackageItemId { get; set; }
       public int PackageId { get; set; }
       public int Qty { get; set; }
       public string Size { get; set; }
   }
}
