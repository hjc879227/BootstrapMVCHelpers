using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Ovixon.Common;
using System.Globalization;
using System.Text;

namespace Ovixon.Bootstrap
{
    public static partial class BootstrapHtmlHelpers
    {
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel = "") where TModel : class
        {
            return htmlHelper.BootstrapDropDownListFor(expression, null, selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "") where TModel : class
        {
            return htmlHelper.BootstrapDropDownListFor(expression, new RouteValueDictionary(htmlAttributes), selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "") where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return htmlHelper.BootstrapDropDownList(inputName, htmlAttributes, selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel = "")
        {
            return BootstrapDropDownList(htmlHelper, name, null /* htmlAttributes */, selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, object htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "")
        {
            return BootstrapDropDownList(htmlHelper, name, new RouteValueDictionary(htmlAttributes), selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "")
        {            
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Name Is Null Or Empty", "name");

            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            var curState = StateElement.Get;
            if (HttpContext.Current.Request.HttpMethod.Equals("POST"))
            {
                curState = StateElement.PostWithOutError;
                ModelState modelState;
                if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
                    if (modelState.Errors.Count > 0)
                        curState = StateElement.PostWithError;
            }

            var defaultValue = htmlHelper.ViewData.Eval(fullName);
            
            if (defaultValue != null)
            {
                var defValue = Convert.ToString(defaultValue, CultureInfo.CurrentCulture);
                List<SelectListItem> newSelectList = new List<SelectListItem>();

                foreach (SelectListItem item in selectList)
                {
                    item.Selected = (item.Value != null) ? item.Value == defValue : item.Text == defValue;
                    newSelectList.Add(item);
                }
                selectList = newSelectList;
            }

            // Convert each ListItem to an <option> tag
            StringBuilder listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem() { Text = optionLabel, Value = String.Empty, Selected = false }));
            }

            foreach (SelectListItem item in selectList)
            {
                listItemBuilder.AppendLine(ListItemToOption(item));
            }

            var tagMainContainer = new TagBuilder("div");
            if (curState == StateElement.Get)
                tagMainContainer.AddCssClass("control-group");
            else if (curState == StateElement.PostWithError)
                tagMainContainer.AddCssClass("control-group error");

            var tagLabel = new TagBuilder("label");
            tagLabel.MergeAttribute("for", name);
            if (htmlHelper.FieldDisplayName(name) != null)
                tagLabel.SetInnerText(htmlHelper.FieldDisplayName(name));
            else
                tagLabel.SetInnerText(name);
            tagLabel.AddCssClass("control-label");

            var tagControlsContainer = new TagBuilder("div");
            tagControlsContainer.AddCssClass("controls");

            var tagInput = htmlHelper.DropDownList(name, selectList, optionLabel, htmlAttributes);

            var helpMessage = htmlHelper.ValidationMessage(name, new { @class = "help-block" }) + (curState == StateElement.PostWithOutError ? "<span class=\"help-block\">&nbsp;</span>" : "");

            tagControlsContainer.InnerHtml = tagInput + "&nbsp;" + helpMessage;
            tagMainContainer.InnerHtml = tagLabel.ToString(TagRenderMode.Normal) + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }

        internal static string ListItemToOption(SelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            return builder.ToString(TagRenderMode.Normal);
        }
    }
}
