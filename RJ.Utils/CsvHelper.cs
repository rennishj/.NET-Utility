using RJ.Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Utils
{
    public static  class CsvHelper
    {
        public static void GenerateCsvFile(List<Person> persons,string delimitor)
        {
            string[] headerColumns = GetAllHeaders();
            string header = string.Join(delimitor, headerColumns);
            //write header
            using (StreamWriter sw = new StreamWriter(@"C\Test\Person.csv"))
            {
                //Writ header
                sw.WriteLine(header);
                //Write columns

            }

        }

        public static string[] GetAllHeaders()
        {
            return new string[] { "PersonId", "FirstName", "LastName" };
        }

        public static void WriteCsv<T>(List<T> items, string path, string delimiter)
        {
            using (StreamWriter sw = new StreamWriter(path,false,Encoding.UTF8))
            {
                string data = GetCsv(items,delimiter);
                sw.WriteAsync(data);
            }
        
        }
       
        
        /// <summary>
        /// This method returns a list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        /// //http://www.joe-stevens.com/2009/08/03/generate-a-csv-from-a-generic-list-of-objects-using-reflection-and-extension-methods/
        public static string GetCsv<T>(this List<T> list,string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] pInfo = typeof(T).GetProperties();
            for (int i = 0; i < pInfo.Length - 1; i++)
            {
                //This is for the header
                sb.Append(pInfo[i].Name);
                if (i < (pInfo.Length - 1))
                {
                    sb.Append(delimiter);
                }
            }
            //data should be added to the next line
            sb.AppendLine();

            //Loop through the collection,then the properties and add the values
            for (int i = 0; i < (list.Count - 1); i++)
            {
                T item = list[i];
                for (int j = 0; j < (pInfo.Length - 1); j++)
                {
                    object propertyValue = item.GetType().GetProperty(pInfo[j].Name).GetValue(item, null);
                    //To do :Add the check for custom CsvIgnore attribute
                    if (propertyValue != null)
                    {
                        string propValueString = propertyValue.ToString();

                        //Check whether the value contains the delimter and ,if so escape that
                        if (propValueString.Contains(delimiter))
                        {
                            propValueString = string.Concat("\"", propertyValue, "\"");
                        }
                        //Replace any \r or \n special characters from a new line with space
                        if (propValueString.Contains("\r"))
                        {
                            propValueString = propValueString.Replace("\r", " ");
                        }
                        if (propValueString.Contains("\n"))
                        {
                            propValueString = propValueString.Replace("\n", " ");
                        }
                        sb.Append(propValueString);
                    }
                    if (j < pInfo.Length - 1)
                    {
                        sb.Append(delimiter);
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
