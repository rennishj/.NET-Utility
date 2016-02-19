using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RJ.MVC.Extensions
{
    public static  class JsonHtmlHelper
    {
        /// <summary>
        /// Main goal of this class is to pass data from razor view to angular js.So we dont need any Htmlencoding
        /// This will make sure that Razor wont Html encode this IHtmlString
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IHtmlString JsonFor<T>(this HtmlHelper helper, T obj)
        {
            return helper.Raw(obj.ToJson());
        }
    }
}