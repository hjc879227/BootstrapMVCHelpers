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
        public static MvcHtmlString BootstrapAlert(this HtmlHelper htmlHelper, string message, string title = "", string startText = "", bool withClose = false, BootstrapStateType stateType = BootstrapStateType.common)
        {
            var tagErrorContainer = new TagBuilder("div");
            tagErrorContainer.AddCssClass("alert");
            if(stateType != BootstrapStateType.common)
                tagErrorContainer.AddCssClass("alert-" + stateType.ToString());

            if (withClose)
            {
                var tagButton = new TagBuilder("button");
                tagButton.AddCssClass("close");
                tagButton.MergeAttribute("data-dismiss", "alert");
                tagButton.InnerHtml = "x";
                tagErrorContainer.InnerHtml += tagButton.ToString(TagRenderMode.Normal);
            }

            if (!string.IsNullOrEmpty(title))
            {
                var tagHead = new TagBuilder("h4");
                tagHead.InnerHtml = title;
                tagErrorContainer.InnerHtml += tagHead.ToString(TagRenderMode.Normal);
            }

            if (!string.IsNullOrEmpty(startText))
            {
                var tagStartText = new TagBuilder("strong");
                tagStartText.InnerHtml = startText;
                tagErrorContainer.InnerHtml += tagStartText.ToString(TagRenderMode.Normal);
            }

            tagErrorContainer.InnerHtml += message;

            return new MvcHtmlString(tagErrorContainer.ToString(TagRenderMode.Normal));
        }
    }

    public enum BootstrapStateType
    {
        common,
        error,
        success,
        info
    }
}
