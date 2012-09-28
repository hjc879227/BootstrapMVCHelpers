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
        public static MvcHtmlString BootstrapClientValidation(this HtmlHelper htmlHelper)
        {
            return new MvcHtmlString(@"
        function CheckForClientValidation() {
            var elems = $('.field-validation-valid.text-error');
            elems.removeClass('text-error');
            elems.parents('.control-group').removeClass('error');

            elems = $('.field-validation-error').not('.text-error');
            elems.addClass('text-error');
            elems.parents('.control-group').addClass('error');

            elems = $('.validation-summary-errors').not('.text-error');
            elems.addClass('alert').addClass('alert-error');

            setTimeout(CheckForClientValidation, 300);
        }
");
        }
    }
}