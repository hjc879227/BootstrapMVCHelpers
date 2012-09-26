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
        public static MvcHtmlString BootstrapDatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool withButton = false, string format = "dd.mm.yyyy") where TModel : class
        {
            return htmlHelper.BootstrapDatepickerFor(expression, null, withButton, format);
        }

        public static MvcHtmlString BootstrapDatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool withButton = false, string format = "dd.mm.yyyy") where TModel : class
        {
            return htmlHelper.BootstrapDatepickerFor(expression, new RouteValueDictionary(htmlAttributes), withButton, format);
        }

        public static MvcHtmlString BootstrapDatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, bool withButton = false, string format = "dd.mm.yyyy") where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return htmlHelper.BootstrapDatepicker(inputName, value, htmlAttributes, withButton, format);
        }

        public static MvcHtmlString BootstrapDatepicker(this HtmlHelper htmlHelper, string name, bool withButton = false, string format = "dd.mm.yyyy")
        {
            return BootstrapDatepicker(htmlHelper, name, null /* value */, withButton, format);
        }

        public static MvcHtmlString BootstrapDatepicker(this HtmlHelper htmlHelper, string name, object value, bool withButton = false, string format = "dd.mm.yyyy")
        {
            return BootstrapDatepicker(htmlHelper, name, value, null /* htmlAttributes */, withButton, format);
        }

        public static MvcHtmlString BootstrapDatepicker(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes, bool withButton = false, string format = "dd.mm.yyyy")
        {
            return BootstrapDatepicker(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes), withButton, format);
        }

        public static MvcHtmlString BootstrapDatepicker(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes, bool withButton = false, string format = "dd.mm.yyyy")
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
            else if (curState == StateElement.PostWithOutError)
                tagMainContainer.AddCssClass("control-group success");
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

            var result = "";
            if (!withButton)
            {
                htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
                htmlAttributes.Add("data-date-format", format);
                htmlAttributes.Add("data-date", DateTime.Now.Date.ToShortDateString());
                if (!htmlAttributes.Keys.Contains("class"))
                    htmlAttributes.Add("class", "bsdatepicker");
                else
                {
                    string temp = htmlAttributes["class"].ToString();
                    temp += " bsdatepicker";
                    htmlAttributes.Remove("class");
                    htmlAttributes.Add("class", temp);
                }
                result = htmlHelper.TextBox(name, value, htmlAttributes).ToString(); 
            }
            else
            {
                htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
                var tagInput = new TagBuilder("div");
                tagInput.AddCssClass("input-append date");
                tagInput.MergeAttribute("data-date-format", format);
                tagInput.MergeAttribute("data-date", DateTime.Now.Date.ToShortDateString());
                tagInput.AddCssClass("bsdatepicker");

                htmlAttributes.Add("readonly", "readonly");
                var tagInput1 = htmlHelper.TextBox(name, value, htmlAttributes).ToString();

                var tagInput2 = new TagBuilder("span");
                tagInput2.AddCssClass("add-on");

                var tagInput3 = new TagBuilder("i");
                tagInput3.AddCssClass("icon-th");

                tagInput2.InnerHtml = tagInput3.ToString(TagRenderMode.Normal);
                tagInput.InnerHtml = tagInput1.ToString() + tagInput2.ToString(TagRenderMode.Normal);
                result = tagInput.ToString(TagRenderMode.Normal);
            }

            var helpMessage = htmlHelper.ValidationMessage(name).ToString();

            tagControlsContainer.InnerHtml = result + "&nbsp;" + helpMessage;
            tagMainContainer.InnerHtml = tagLabel.ToString(TagRenderMode.Normal) + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }
    }
}
