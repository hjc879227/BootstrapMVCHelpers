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
            return htmlHelper.ValidationSummary(message, htmlAttributes);
        }
    }
}