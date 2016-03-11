using RJ.PizzaOrderingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RJ.PizzaOrderingService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
   public class ZzaService : IZzaService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SubmitOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product{Id = 1,Name="The Works",Type = "Large",Description = "The works Pizza"},
                new Product{Id = 2,Name="Double Cheese",Type = "Small",Description = "Double Cheese Pizza"},
                new Product{Id = 3,Name="Garden Pizza",Type = "Medium",Description = "GerdenPizza"}
            };
            return products;
        }

        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>()
            {
                new Customer{Id = Guid.NewGuid(),FirstName = "Rennish",LastName = "Joseph",Phone = "4077892563",State = "FL",Zip = "388789",Street = "Mainstreet"},
                new Customer{Id = Guid.NewGuid(),FirstName = "Nathan",LastName = "Joseph",Phone = "4077892563",State = "FL",Zip = "388789",Street = "Mainstreet"},
                new Customer{Id = Guid.NewGuid(),FirstName = "Anupama",LastName = "Joseph",Phone = "4077892563",State = "FL",Zip = "388789",Street = "Mainstreet"}
            };
            return customers;
        }
    }
}
