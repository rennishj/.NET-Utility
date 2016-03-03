using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RJ.Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Utils
{
   public  class JsonHelper
    {
       public static object Deserialize<T>(string filePath)
       {
           filePath = @"C:\DotNetApplications\GitRepos\NetUtility\NetUtility\RJ.ConsoleTest\JsonFile\order.json";
           using (var sr = new StreamReader(filePath))
           {
               var jsonString = sr.ReadToEnd();
               var obj = JsonConvert.DeserializeObject<T>(jsonString);
               return obj;
           }
       }

       public static string JsonSerialize<T>(T obj)
       {
           var jsonSerialserSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
           var jsonString = JsonConvert.SerializeObject(obj, jsonSerialserSettings);
           return jsonString;
       }
    }
}
