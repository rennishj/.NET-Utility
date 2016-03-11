using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.PizzaService.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ZzaServices.ZzaServiceClient("BasicHttpBinding_IZzaService");
            client.ClientCredentials.Windows.ClientCredential.UserName = "rennishj";
            client.ClientCredentials.Windows.ClientCredential.Password = "N@than2012";
            var result = client.GetCustomers();
            foreach (var item in result)
            {
                Console.WriteLine(item.FirstName);
            }
            Console.ReadKey();
        }
    }
}
