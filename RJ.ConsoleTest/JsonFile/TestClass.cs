using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.ConsoleTest.JsonFile
{

    public class Rootobject
    {
        public Order order { get; set; }
    }

    public class Order
    {
        public int orderId { get; set; }
        public string orderDate { get; set; }
        public float orderTotal { get; set; }
        public Customer customer { get; set; }
        public List<Packages> packages { get; set; }
    }

    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }

    public class Packages
    {
        public int packageId { get; set; }
        public string shipDate { get; set; }
        public int orderId { get; set; }
        public string promiseDate { get; set; }
        public Packageitem[] packageItems { get; set; }
    }

    public class Packageitem
    {
        public int packageItemId { get; set; }
        public int packageId { get; set; }
        public int qty { get; set; }
        public string size { get; set; }
    }

}
