using RJ.Poco;
using RJ.Utils;
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
            try
            {
                CsvHelper.WriteCsv<Person>(GetPersons(), @"C:\Test\Person.csv","|");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Finished wrting to the file");
            Console.ReadKey();

        }

        private static void PrintHeader()
        {
            string[] columns = new string[] { "PersonId", "FirstName", "LastName" };
            string header = string.Join("|", columns);
            Console.WriteLine(header);
        }

        private static List<Person> GetPersons()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                persons.Add(new Person
                {
                    FirstName = "Rennish_" + i.ToString(),
                    LastName = "Joseph_" + i.ToString(),
                    PersonId = i,
                    ZipCode = "32225_" + i.ToString()
                });
            }
            return persons;
        }
    }

    public enum ConsoleActions
    { 
        PrintHeader = 1,
    }
}
