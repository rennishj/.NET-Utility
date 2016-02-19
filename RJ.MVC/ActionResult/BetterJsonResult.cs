using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RJ.MVC.Extensions;

namespace RJ.MVC.CustomActionResult
{
    /// <summary>
    /// These are all taken from Matt Honeycutts Pluralsight course on Angularjs MVC5 course
    /// </summary>
    public class BetterJsonResult : JsonResult
    {
        public IList<string> ErrorMessages { get; set; }
        public BetterJsonResult()
        {
            this.ErrorMessages = new List<string>();
        }
        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        public override void ExecuteResult(ControllerContext context)
        {
                base.ExecuteResult(context);
                SerializeData(context.HttpContext.Response);
        }

        private void DoUninterestingBaseClassStuff(ControllerContext context)
        {
            if (context == null)
            { 
            }
        }
        protected virtual void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                Data = new
                {
                    ErrorMessage  = string.Join("\n" ,ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };
                response.StatusCode = 400;
            }
            if (Data == null)
                return;
            response.Write(Data.ToJson());
        }
    }

    public class BetterJsonResult<T> : BetterJsonResult
    {
        /// <summary>
        /// we want to hide the JsonResults public object Data { get; set; } with this property
        /// </summary>
        public new T Data
        {
            get
            {
                return (T)base.Data;
            }
            set
            {
                base.Data = value;
            }
        }
    }
}