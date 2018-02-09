using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Models
{
    public class PreventSpam
    {
        //public static bool IsDuplicateRequest(int buyorderid, string buyorderguid, int orderType)
        public static bool IsDuplicateRequest(string param1, string param2)
        {
            bool result = false;
            var cancelRequestData = string.Format("{0}{1}", param1, param2);
            var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(cancelRequestData)).Select(s => s.ToString("x2")));
            
            var cache = MemoryCache.Default;
            if (cache.Get(hashValue) == null)
            {
                var cachePolicty = new CacheItemPolicy();
                cachePolicty.AbsoluteExpiration = DateTime.Now.AddMinutes(2);
                cache.Add(hashValue, cancelRequestData, cachePolicty);
                result = true;
            }
            return result;
        }
    }
}