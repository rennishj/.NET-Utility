using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace RJ.BLL
{
   public class CacheService
    {
       public static ObjectCache NetUtilsCache
       {
           get
           {
               return MemoryCache.Default;
           }
       }
       public static void Put(object obj,string key,CacheItemPolicy policy)
       {
           var cacheObj = NetUtilsCache[key] as object;
           if (cacheObj == null)
           {
               NetUtilsCache.Set(key, obj, policy);
           }
       }

       public static Object Get(string key)
       {
           return NetUtilsCache[key];
       }
    }
}
