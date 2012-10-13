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
        public static MvcHtmlString BootstrapStyledDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel = "") where TModel : class
        {
            return htmlHelper.BootstrapStyledDropDownListFor(expression, null, selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapStyledDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "") where TModel : class
        {
            return htmlHelper.BootstrapStyledDropDownListFor(expression, new RouteValueDictionary(htmlAttributes), selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapStyledDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "") where TModel : class
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            TProperty value = CommonHtmlHelpers.GetValue(htmlHelper, expression);
            return htmlHelper.BootstrapStyledDropDownList(inputName, htmlAttributes, selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapStyledDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel = "")
        {
            return BootstrapStyledDropDownList(htmlHelper, name, null /* htmlAttributes */, selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapStyledDropDownList(this HtmlHelper htmlHelper, string name, object htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "")
        {
            return BootstrapStyledDropDownList(htmlHelper, name, new RouteValueDictionary(htmlAttributes), selectList, optionLabel);
        }

        public static MvcHtmlString BootstrapStyledDropDownList(this HtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes, IEnumerable<SelectListItem> selectList, string optionLabel = "")
        {            
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Name Is Null Or Empty", "name");

            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

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

            List<SelectListItem> selectListTemp = new List<SelectListItem>();

            if (optionLabel != null)
            {
                htmlAttributes.Add("data-placeholder", optionLabel);
                selectListTemp.Add(new SelectListItem() { Text = String.Empty, Value = String.Empty, Selected = false });
            }

            foreach (SelectListItem item in selectList)
            {
                selectListTemp.Add(item);
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

            if (!htmlAttributes.Keys.Contains("class"))
                htmlAttributes.Add("class", "chzn-select");
            else
            {
                string temp = htmlAttributes["class"].ToString();
                temp += " chzn-select";
                htmlAttributes.Remove("class");
                htmlAttributes.Add("class", temp);
            }


            var tagInput = htmlHelper.DropDownList(name, selectListTemp, htmlAttributes);

            var helpMessage = htmlHelper.ValidationMessage(name, new { @class = "help-block" }) + (curState == StateElement.PostWithOutError ? "<span class=\"help-block\">&nbsp;</span>" : "");

            tagControlsContainer.InnerHtml = tagInput + "&nbsp;" + helpMessage;
            tagMainContainer.InnerHtml = tagLabel.ToString(TagRenderMode.Normal) + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }
    }
}
