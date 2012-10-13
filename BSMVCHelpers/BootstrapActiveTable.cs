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
        public static MvcHtmlString BootstrapActiveTable(this HtmlHelper htmlHelper, string ID, List<List<string>> elements, List<string> headers = null, List<string> footers = null,
            BootstrapTableStyles tableStyles = null, List<BootstrapRowStyleType> rowStyles = null)
        {
            if (tableStyles == null)
                tableStyles = new BootstrapTableStyles();

            var tagTableContainer = new TagBuilder("table");
            tagTableContainer.MergeAttribute("ID", ID);
            if (!string.IsNullOrEmpty(tableStyles.Caption))
                tagTableContainer.InnerHtml += "<caption>" + tableStyles.Caption + "</caption>";
            tagTableContainer.AddCssClass("table");
            if (tableStyles.Bordered)
                tagTableContainer.AddCssClass("table-bordered");
            if (tableStyles.Striped)
                tagTableContainer.AddCssClass("table-striped");
            if (tableStyles.Hover)
                tagTableContainer.AddCssClass("table-hover");
            if (tableStyles.Condensed)
                tagTableContainer.AddCssClass("table-condensed");

            if (headers != null)
            {
                var tagHeadContainer = new TagBuilder("thead");
                var tagHeadTRContainer = new TagBuilder("tr");
                foreach (string header in headers)
                    tagHeadTRContainer.InnerHtml += "<th>" + header + "</th>";
                tagHeadContainer.InnerHtml += tagHeadTRContainer.ToString(TagRenderMode.Normal);
                tagTableContainer.InnerHtml += tagHeadContainer.ToString(TagRenderMode.Normal);
            }

            int i = 0;
            var tagBodyContainer = new TagBuilder("tbody");
            foreach (List<string> row in elements)
            {
                var tagBodyTRContainer = new TagBuilder("tr");
                foreach (string cell in row)
                    tagBodyTRContainer.InnerHtml += "<td>" + cell + "</td>";
                if (rowStyles != null && rowStyles[i] != BootstrapRowStyleType.none)
                    tagBodyTRContainer.AddCssClass(rowStyles[i].ToString());
                tagBodyContainer.InnerHtml += tagBodyTRContainer.ToString(TagRenderMode.Normal);
                i++;
            }
            tagTableContainer.InnerHtml += tagBodyContainer.ToString(TagRenderMode.Normal);

            if (footers != null)
            {
                var tagFootContainer = new TagBuilder("tfoot");
                var tagFootTRContainer = new TagBuilder("tr");
                foreach (string footer in footers)
                    tagFootTRContainer.InnerHtml += "<th>" + footer + "</th>";
                tagFootContainer.InnerHtml += tagFootTRContainer.ToString(TagRenderMode.Normal);
                tagTableContainer.InnerHtml += tagFootContainer.ToString(TagRenderMode.Normal);
            }

            return new MvcHtmlString(tagTableContainer.ToString(TagRenderMode.Normal));
        }
    }
}
