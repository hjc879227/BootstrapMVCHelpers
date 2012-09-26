using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Ovixon.Bootstrap
{
    public static partial class BootstrapHtmlHelpers
    {
        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper htmlHelper)
        {
            return BootstrapValidationSummary(htmlHelper, null /* message */);
        }

        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper htmlHelper, string message)
        {
            return BootstrapValidationSummary(htmlHelper, message, null);
        }

        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper htmlHelper, string message, object htmlAttributes)
        {
            return BootstrapValidationSummary(htmlHelper, message, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper htmlHelper, string message, IDictionary<string, object> htmlAttributes)
        {
            /*// Nothing to do if there aren't any errors
            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                return null;
            }

            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            if (!htmlAttributes.Keys.Contains("class"))
                htmlAttributes.Add("class", "alert alert-error");
            else
            {
                string temp = htmlAttributes["class"].ToString();
                temp += " alert alert-error";
                htmlAttributes.Remove("class");
                htmlAttributes.Add("class", temp);
            }*/
            return htmlHelper.ValidationSummary(message, htmlAttributes);
        }
    }
}