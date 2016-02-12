using RJ.Poco;
using RJ.Poco.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                string data = GetCsv(items,delimiter);
                sw.WriteLine(data);
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
            PropertyInfo[] pInfo = typeof(T).GetProperties().Where(p => { return p.GetCustomAttribute<CsvIgnore>() == null; }).ToArray();
            for (int i = 0; i <= (pInfo.Length - 1); i++)
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
                for (int j = 0; j <= (pInfo.Length - 1); j++)
                {
                    PropertyInfo property = item.GetType().GetProperty(pInfo[j].Name);
                    object propertyValue = property.GetValue(item, null);
                    CsvIgnore csvIgnore = property.GetCustomAttribute(typeof(CsvIgnore)) as CsvIgnore;
                    //To do :Add the check for custom CsvIgnore attribute
                    if (propertyValue != null && csvIgnore == null)
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
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        /// <summary>
        /// This is used to split large csv files into two separate files
        /// </summary>
        /// <param name="filePath"></param>
        public static void ReportBurstFiles(string filePath,string delimiter)
        {
            long length = (new FileInfo(filePath).Length / 1024);
            string[] header = null;
            string headerRow = null;
            int? previousPersonId = null;
            int? currentPersonId = null;
            string currentLine = null;
            string burstedFilePath = null;
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(filePath))
            {
                headerRow = sr.ReadLine();
                if (headerRow != null)
                {
                    header = headerRow.Split(new string[]{delimiter}, StringSplitOptions.RemoveEmptyEntries);
                    sb.Append(headerRow);
                }
                while (!sr.EndOfStream)
                {
                    currentLine = sr.ReadLine();
                    if (!string.IsNullOrWhiteSpace(currentLine))
                    {
                        string[] row = currentLine.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                        currentPersonId = Convert.ToInt32(row[0]);
                        if (previousPersonId == null || currentPersonId.Value == previousPersonId.Value)
                        { 
                         //data belongs to the same person
                            previousPersonId = currentPersonId;
                            if (sb == null)
                            {
                                sb = new StringBuilder();
                            }
                            sb.AppendLine();
                            sb.Append(currentLine);
                        }
                        else if (previousPersonId != null && currentPersonId.Value != previousPersonId.Value)
                        { 
                            //This is the start of the new person data,so write the previous data
                            burstedFilePath = @"C:\Test\" + string.Format("{0}.{1}.{2}", previousPersonId.Value, DateTime.Now.ToString("ddMMyyyyhhmmss"),"csv");
                            CreateTheCsvFile(burstedFilePath, sb.ToString());
                            sb = null;
                            //Get the new row details
                            sb = new StringBuilder();
                            sb.Append(headerRow);
                            sb.AppendLine();
                            sb.Append(currentLine);
                            previousPersonId = currentPersonId;
                            burstedFilePath = @"C:\Test\" + string.Format("{0}.{1}.{2}", currentPersonId.Value, DateTime.Now.ToString("ddMMyyyyhhmmss"), "csv");
                            currentPersonId = null;                            
                        }
                        
                    }
                }
                //write the last persons data to file
                CreateTheCsvFile(burstedFilePath, sb.ToString());
            }
        }

        private static  void CreateTheCsvFile(string fileName,string data)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }                
                sw.WriteLine(data);
            }
        }
    }
}
