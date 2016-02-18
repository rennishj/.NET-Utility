using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace RJ.MVC.Extensions
{
    /// <summary>
    /// This is a helper for Bootstrap3 that can be used inplace for the default MVC5 helper methods
    /// </summary>
    public  static class BootStrapExtension
    {
        public static IHtmlString BSInputFor<TModel,TProperty>(this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel,TProperty>> expression,
                HtmlInputType inputType,
                IDictionary<string,string> options = null,
                object htmlAttributes = null,
                bool includeLabel = true)
        {
            var model = htmlHelper.ViewData.Model;
            var fieldValue = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).SimpleDisplayText;
            var fieldValueExact = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var propertyBuilder = new StringBuilder();
            if (htmlAttributes != null)
            { 
                Type t = htmlAttributes.GetType();
                foreach (var property in t.GetProperties())
	            {
		            propertyBuilder.AppendFormat("{0}='{1}'",property.Name.Replace("_","-"),property.GetValue(htmlAttributes,null));
	            }
            }

            var required = model.GetAttributeFrom<RequiredAttribute>(fieldName);
            var email = model.GetAttributeFrom<EmailAddressAttribute>(fieldName);
            var maxLength = model.GetAttributeFrom<MaxLengthAttribute>(fieldName);
            var display = model.GetAttributeFrom<DisplayAttribute>(fieldName);
            var range = model.GetAttributeFrom<RangeAttribute>(fieldName);
            var regEx = model.GetAttributeFrom<RegularExpressionAttribute>(fieldName);
            var displayFormat = model.GetAttributeFrom<DisplayFormatAttribute>(fieldName);

            var hasRequired = required != null;
            var errorMessage = BuildErrorMessage(required,email);
            //Need to revist this
            var sb = new StringBuilder();
            var formattedFieldValue = displayFormat != null ? string.Format(displayFormat.DataFormatString,fieldValueExact) : fieldValue;
            string formatString = null;
            switch(inputType)
            {
                case HtmlInputType.Text:
                    formatString = hasRequired ? "<label class='control-label' for='{0}'>{1}</label><input class='form-control' type='{4}'{3} {2} {5} /><span class='field-validation-valid' {6} {7}><span {8} {9}></span></span>": null;
                    sb.AppendFormat(formatString,
                        /*0 fieldName*/ fieldName,
                        /*1 fieldName*/ fieldName,                                  
                        /*2 data-val*/ hasRequired ?  string.Format("{0}='{1}'","data-val",true) : null,
                        /*3 data-val-required*/  hasRequired ? string.Format("{0}='{1}'","data-val-required",errorMessage) : null,
                        /*4 type*/ Enum.GetName(typeof(HtmlInputType),inputType).ToString().ToLower(),
                        /*5*/ propertyBuilder.ToString(),
                        /*6*/hasRequired ? string.Format("{0}='{1}'","data-valmsg-for",fieldName) : null,
                        /*7*/hasRequired ? string.Format("{0}='{1}'","data-valmsg-replace",fieldName,true) : null
                        );
                    break;

                
            }
            //Add ur stuff
            var hasValidation = required != null || email != null;
            return new HtmlString(sb.ToString());
        }

        private static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attributeType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            if (property != null)
            {
                return (T)property.GetCustomAttributes<T>(false).FirstOrDefault();
            }
            return default(T);
        }

        /// <summary>
        /// Feel free to add more attributes as and needed
        /// </summary>
        /// <param name="required"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private static string BuildErrorMessage(RequiredAttribute required,EmailAddressAttribute email)
        {
            var erorMessage = new List<string>();
            if (required != null && !string.IsNullOrWhiteSpace(required.ErrorMessage))
            {
                erorMessage.Add(required.ErrorMessage);
            }
            if (email != null && !string.IsNullOrWhiteSpace(email.ErrorMessage))
            {
                erorMessage.Add(email.ErrorMessage);
            }
            if (erorMessage.Count > 0)
            {
                return string.Join(" ", erorMessage);
            }
            return string.Empty;
        }
    }

    public enum HtmlInputType
    { 
        Text = 1,
        Email,
        DropdownList,
        CheckBox,
        RadioButton,
        TextArea,
        Hidden,
        Button,
        Submit,
        AnchorTag,

    }
}