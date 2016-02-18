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

            var errorMessage = BuildErrorMessage(required,email);
            //Need to revist this
            var formattedFieldValue = displayFormat != null ? string.Format(displayFormat.DataFormatString,fieldValueExact) : fieldValue;
            string formatString = null;
            switch(inputType)
            {
                case HtmlInputType.Text:
                    formatString = includeLabel ? "<label class='control-label' for='{0}'>{1}</label><input class='form-control'></input>": null;
                    break;

                
            }
            //Add ur stuff
            var hasValidation = required != null || email != null;
            return null;
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