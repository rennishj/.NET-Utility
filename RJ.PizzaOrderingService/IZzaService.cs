using RJ.PizzaOrderingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RJ.PizzaOrderingService
{
    [ServiceContract]
   public interface IZzaService
    {
        [OperationContract]
        void SubmitOrder(Order order);
        
        [OperationContract]
        List<Product> GetProducts();
        
        [OperationContract]
        List<Customer> GetCustomers();
    }
}
