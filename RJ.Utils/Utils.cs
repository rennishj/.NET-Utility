using RJ.Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Utils
{
    public static  class Utils
    {
        public static void GenerateCsvFile(List<Person> persons,string delimiter)
        {
            string[] headerColumns = GetAllHeaders();
            string header = string.Join("|",headerColumns);
            //write header
            using (StreamWriter sw = new StreamWriter(@"C\Test\Person.csv"))
            {
                //sw.WriteLine()
            }

        }

        public static string[] GetAllHeaders()
        {
            return new string[] { "PersonId", "FirstName", "LastName" };
        }
    }
}
