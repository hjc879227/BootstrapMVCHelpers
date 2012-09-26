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
        public static MvcHtmlString BootstrapTooltip(this HtmlHelper htmlHelper, string message, string tooltipMessage, BootstrapTooltipType tooltipType = BootstrapTooltipType.top)
        {
            var tagTooltipContainer = new TagBuilder("a");
            tagTooltipContainer.AddCssClass("bstooltip");
            tagTooltipContainer.MergeAttribute("data-original-title", tooltipMessage);
            tagTooltipContainer.MergeAttribute("href", "#");
            tagTooltipContainer.MergeAttribute("rel", "tooltip");
            tagTooltipContainer.MergeAttribute("data-placement", tooltipType.ToString());
            tagTooltipContainer.InnerHtml = message;

            return new MvcHtmlString(tagTooltipContainer.ToString(TagRenderMode.Normal));
        }
    }

    public enum BootstrapTooltipType
    {
        top,
        left,
        right,
        bottom
    }
}
