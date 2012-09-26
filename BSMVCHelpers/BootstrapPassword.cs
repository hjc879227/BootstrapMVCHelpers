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
        public static MvcHtmlString BootstrapPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            return htmlHelper.BootstrapPasswordFor(expression, null);
        }

        public static MvcHtmlString BootstrapPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes) where TModel : class
        {
            return htmlHelper.BootstrapPasswordFor(expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes) where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return htmlHelper.BootstrapPassword(inputName, value, htmlAttributes);
        }

        public static MvcHtmlString BootstrapPassword(this HtmlHelper htmlHelper, string name)
        {
            return BootstrapPassword(htmlHelper, name, null /* value */);
        }

        public static MvcHtmlString BootstrapPassword(this HtmlHelper htmlHelper, string name, object value)
        {
            return BootstrapPassword(htmlHelper, name, value, null /* htmlAttributes */);
        }

        public static MvcHtmlString BootstrapPassword(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return BootstrapPassword(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapPassword(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
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

            var tagLabel = new TagBuilder("label");
            tagLabel.MergeAttribute("for", name);
            if (htmlHelper.FieldDisplayName(name) != null)
                tagLabel.SetInnerText(htmlHelper.FieldDisplayName(name));
            else
                tagLabel.SetInnerText(name);
            tagLabel.AddCssClass("control-label");

            var tagControlsContainer = new TagBuilder("div");
            tagControlsContainer.AddCssClass("controls");

            var tagInput = htmlHelper.Password(name, value, htmlAttributes);

            var helpMessage = htmlHelper.ValidationMessage(name).ToString();

            tagControlsContainer.InnerHtml = tagInput.ToString() + "&nbsp;" + helpMessage;
            tagMainContainer.InnerHtml = tagLabel.ToString(TagRenderMode.Normal) + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }
    }
}
