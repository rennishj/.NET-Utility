using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using RJ.PizzaOrderingService;

namespace RJ.PizzaConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = new ServiceHost(typeof(ZzaService));
                host.Open();
                Console.WriteLine("Service is up and running");
                Console.ReadKey();
                host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }        
    }
}
