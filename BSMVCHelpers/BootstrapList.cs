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
        public static MvcHtmlString BootstrapList(this HtmlHelper htmlHelper, List<string> elements, BootstrapListType listType = BootstrapListType.ordered)
        {
            var tagListContainer = listType == BootstrapListType.ordered ? new TagBuilder("ol") : new TagBuilder("ul");
            if(listType == BootstrapListType.unstyled)
                tagListContainer.AddCssClass("unstyled");

            foreach (string elem in elements)
            {
                tagListContainer.InnerHtml += "<li>" + elem + "</li>";
            }

            return new MvcHtmlString(tagListContainer.ToString(TagRenderMode.Normal));
        }
    }

    public enum BootstrapListType
    {
        ordered,
        unordered,
        unstyled
    }
}
