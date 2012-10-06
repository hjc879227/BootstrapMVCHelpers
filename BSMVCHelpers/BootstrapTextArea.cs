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
        private const int TextAreaRows = 2;
        private const int TextAreaColumns = 20;

        public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            return htmlHelper.BootstrapTextAreaFor(expression, null);
        }

        public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes) where TModel : class
        {
            return htmlHelper.BootstrapTextAreaFor(expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes) where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return BootstrapTextAreaSpec(htmlHelper, inputName, value, TextAreaRows, TextAreaColumns, htmlAttributes, false);
        }

        public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int textAreaRows, int textAreaColumns, IDictionary<string, object> htmlAttributes) where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return BootstrapTextAreaSpec(htmlHelper, inputName, value, textAreaRows, textAreaColumns, htmlAttributes, true);
        }

        public static MvcHtmlString BootstrapTextArea(this HtmlHelper htmlHelper, string name)
        {
            return BootstrapTextArea(htmlHelper, name, null /* value */);
        }

        public static MvcHtmlString BootstrapTextArea(this HtmlHelper htmlHelper, string name, object value)
        {
            return BootstrapTextArea(htmlHelper, name, value, null /* htmlAttributes */);
        }

        public static MvcHtmlString BootstrapTextArea(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return BootstrapTextArea(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapTextArea(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return BootstrapTextAreaSpec(htmlHelper, name, value, TextAreaRows, TextAreaColumns, htmlAttributes, false);
        }

        public static MvcHtmlString BootstrapTextArea(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes, int textAreaRows, int textAreaColumns)
        {
            return BootstrapTextAreaSpec(htmlHelper, name, value, textAreaRows, textAreaColumns, htmlAttributes, true);
        }

        private static MvcHtmlString BootstrapTextAreaSpec(HtmlHelper htmlHelper, string name, object value, int textAreaRows, int textAreaColumns, IDictionary<string, object> htmlAttributes, bool IsOuterIntParam)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("{0} Is Null Or Empty", "name");
            }
            if (textAreaRows <= 0)
            {
                throw new ArgumentOutOfRangeException("rows", "Argument {0} Out Of Range Exception");
            }
            if (textAreaColumns <= 0)
            {
                throw new ArgumentOutOfRangeException("columns", "Argument {0} Out Of Range Exception");
            }
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

            var tagLabel = new TagBuilder("label");
            tagLabel.MergeAttribute("for", name);
            if (htmlHelper.FieldDisplayName(name) != null)
                tagLabel.SetInnerText(htmlHelper.FieldDisplayName(name));
            else
                tagLabel.SetInnerText(name);
            tagLabel.AddCssClass("control-label");

            var tagControlsContainer = new TagBuilder("div");
            tagControlsContainer.AddCssClass("controls");

            var valueStr = value == null ? "" : value.ToString();
            var tagTextArea = (IsOuterIntParam ? htmlHelper.TextArea(name, valueStr, textAreaRows, textAreaColumns, htmlAttributes) :
                htmlHelper.TextArea(name, valueStr, htmlAttributes));

            var helpMessage = htmlHelper.ValidationMessage(name, new { @class = "help-block" }) + (curState == StateElement.PostWithOutError ? "<span class=\"help-block\">&nbsp;</span>" : "");

            tagControlsContainer.InnerHtml = tagTextArea.ToString() + "&nbsp;" + helpMessage;
            tagMainContainer.InnerHtml = tagLabel.ToString(TagRenderMode.Normal) + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }
    }
}
