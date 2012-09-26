using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Ovixon.Common;
using System.Globalization;

namespace Ovixon.Bootstrap
{
    public static partial class BootstrapHtmlHelpers
    {
        public static MvcHtmlString BootstrapDescriptions(this HtmlHelper htmlHelper, Dictionary<string, string> elements, bool horizontalView = false)
        {
            var tagDescriptionContainer = new TagBuilder("dl");
            if (horizontalView)
                tagDescriptionContainer.AddCssClass("dl-horizontal");

            foreach (KeyValuePair<string, string> elem in elements)
            {
                tagDescriptionContainer.InnerHtml += "<dt>" + elem.Key + "</dt>" + "<dd>" + elem.Value + "</dd>";
            }

            return new MvcHtmlString(tagDescriptionContainer.ToString(TagRenderMode.Normal));
        }
    }
}
