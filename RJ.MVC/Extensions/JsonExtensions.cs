using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace RJ.MVC.Extensions
{
    /// <summary>
    /// This converts C# pascal case to Json's Camelcase
    /// </summary>
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj,bool includeNull = true)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] { new StringEnumConverter() },
                NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}