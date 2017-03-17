using System;
using System.Web;

namespace Common
{
    public class DataCache
    {
        public static object GetCache(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }

        public static void SetCache(string cacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(cacheKey, objObject);
        }

        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        public static void RemoveCache(string cacheKey)
        {
            HttpRuntime.Cache.Remove(cacheKey);
        }
    }
}
