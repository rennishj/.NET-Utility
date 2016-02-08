using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Console Actions");
            int axn = Int32.Parse(Console.ReadLine());
            switch (axn)
            {
                case 1:
                    PrintHeader();
                    break;
                default:
                    break;
            }
            Console.ReadKey();

        }

        private static void PrintHeader()
        {
            string[] columns = new string[] { "PersonId", "FirstName", "LastName" };
            string header = string.Join("|", columns);
            Console.WriteLine(header);
        }
    }

    public enum ConsoleActions
    { 
        PrintHeader = 1,
    }
}
