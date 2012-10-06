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

namespace Ovixon.Bootstrap
{
    public static partial class BootstrapHtmlHelpers
    {
        public static MvcHtmlString BootstrapCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string helpText = "") where TModel : class
        {
            return htmlHelper.BootstrapCheckBoxFor(expression, null, helpText);
        }

        public static MvcHtmlString BootstrapCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, string helpText = "") where TModel : class
        {
            return htmlHelper.BootstrapCheckBoxFor(expression, new RouteValueDictionary(htmlAttributes), helpText);
        }

        public static MvcHtmlString BootstrapCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, string helpText = "") where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return htmlHelper.BootstrapCheckBox(inputName, bool.Parse(value.ToString()), htmlAttributes, helpText);
        }

        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper htmlHelper, string name, string helpText = "")
        {
            return BootstrapCheckBox(htmlHelper, name, false /* value */, helpText);
        }

        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, string helpText = "")
        {
            return BootstrapCheckBox(htmlHelper, name, isChecked, null /* htmlAttributes */, helpText);
        }

        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object htmlAttributes, string helpText = "")
        {
            return BootstrapCheckBox(htmlHelper, name, isChecked, new RouteValueDictionary(htmlAttributes), helpText);
        }

        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, IDictionary<string, object> htmlAttributes, string helpText = "")
        {            
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Name Is Null Or Empty", "name");

            var curState = StateElement.Get;
            if (HttpContext.Current.Request.HttpMethod.Equals("POST"))
            {
                curState = StateElement.PostWithOutError;
                ModelState modelState;
                if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
                    if (modelState.Errors.Count > 0)
                        curState = StateElement.PostWithError;
            }

            var tagMainContainer = new TagBuilder("div");
            if (curState == StateElement.Get)
                tagMainContainer.AddCssClass("control-group");
            else if (curState == StateElement.PostWithError)
                tagMainContainer.AddCssClass("control-group error");

            var tagLabelText = "";
            if (string.IsNullOrEmpty(helpText))
            {
                var tagLabel = new TagBuilder("label");
                tagLabel.MergeAttribute("for", name);
                if (htmlHelper.FieldDisplayName(name) != null)
                    tagLabel.SetInnerText(htmlHelper.FieldDisplayName(name));
                else
                    tagLabel.SetInnerText(name);
                tagLabel.AddCssClass("control-label");
                tagLabelText = tagLabel.ToString(TagRenderMode.Normal);
            }

            var tagControlsContainer = new TagBuilder("div");
            tagControlsContainer.AddCssClass("controls");

            var helpMessage = htmlHelper.ValidationMessage(name, new {@class = "help-block"}).ToString() + (curState == StateElement.PostWithOutError ? "<span class=\"help-block\">&nbsp;</span>" : "");

            var tagInput = htmlHelper.CheckBox(name, isChecked, htmlAttributes).ToString();
            if (!string.IsNullOrEmpty(helpText))
                tagInput = "<label class='checkbox'>" + tagInput + "&nbsp;" + helpText + "&nbsp;" + helpMessage + "</label>";
            else
                tagInput = "<label class='checkbox'>" + tagInput + "&nbsp;" + helpMessage + "</label>";

            tagControlsContainer.InnerHtml = tagInput;
            tagMainContainer.InnerHtml = tagLabelText + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }
    }
}
