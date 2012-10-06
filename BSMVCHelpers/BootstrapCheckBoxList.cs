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
        //http://codable.ru/kirpichiki-dlya-asp-net-mvc-4-checkboxlistfor-i-svoj-generator-view.html
        /*public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(
               this HtmlHelper<TModel> htmlHelper,
               Expression<Func<TModel, TProperty[]>> expression,
               MultiSelectList multiSelectList,
               object htmlAttributes = null)
        {
            MemberExpression body = expression.Body as MemberExpression;
            string propertyName = body.Member.Name;
            TProperty[] list = expression.Compile().Invoke(htmlHelper.ViewData.Model);
            List<string> selectedValues = new List<string>();
            if (list != null)
            {
                selectedValues = new List<TProperty>(list).ConvertAll<string>(delegate(TProperty i)
                { return i.ToString(); });
            }

            TagBuilder divTag = new TagBuilder("div class=\"CheckBoxListFor\"");
            divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
            foreach (SelectListItem item in multiSelectList)
            {
                divTag.InnerHtml += String.Format("<label><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
                                                    "value=\"{1}\" {2} />{3}</label>",
                                                    propertyName,
                                                    item.Value,
                                                    selectedValues.Contains(item.Value) ? "checked=\"checked\"" : "",
                                                    item.Text);
            }
            return MvcHtmlString.Create(divTag.ToString());
        }*/

        public static MvcHtmlString BootstrapCheckBoxListFor<TModel, TProperty>(
               this HtmlHelper<TModel> htmlHelper,
               Expression<Func<TModel, TProperty[]>> expression,
               MultiSelectList multiSelectList,
               object htmlAttributes = null) where TModel : class
        {
            return htmlHelper.BootstrapCheckBoxListFor(expression, multiSelectList, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapCheckBoxListFor<TModel, TProperty>(
                this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty[]>> expression,
                MultiSelectList multiSelectList,
                IDictionary<string, object> htmlAttributes) where TModel : class
        {

            string name = ExpressionHelper.GetExpressionText(expression);
            TProperty[] list = expression.Compile().Invoke(htmlHelper.ViewData.Model);
            List<string> selectedValues = new List<string>();
            if (list != null)
            {
                selectedValues = new List<TProperty>(list).ConvertAll<string>(delegate(TProperty i)
                { return i.ToString(); });
            }
            return htmlHelper.BootstrapCheckBoxList(name, selectedValues, multiSelectList, htmlAttributes);
        }

        public static MvcHtmlString BootstrapCheckBoxList(
               this HtmlHelper htmlHelper,
               string name,
               List<string> selectedValues,
               MultiSelectList multiSelectList,
               object htmlAttributes = null)
        {
            return htmlHelper.BootstrapCheckBoxList(name, selectedValues, multiSelectList, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BootstrapCheckBoxList(
                this HtmlHelper htmlHelper,
                string name,
                List<string> selectedValues,
                MultiSelectList multiSelectList,
                IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Name Is Null Or Empty", "name");

            var tagMainContainer = new TagBuilder("div");
            tagMainContainer.AddCssClass("control-group");

            var tagLabel = new TagBuilder("label");
            tagLabel.MergeAttribute("for", name);
            if (htmlHelper.FieldDisplayName(name) != null)
                tagLabel.SetInnerText(htmlHelper.FieldDisplayName(name));
            else
                tagLabel.SetInnerText(name);
            tagLabel.AddCssClass("control-label");

            var tagControlsContainer = new TagBuilder("div");
            tagControlsContainer.AddCssClass("controls");

            //tagControlsContainer.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
            foreach (SelectListItem item in multiSelectList)
            {
                var tagChBL = new TagBuilder("label");
                tagChBL.AddCssClass("checkbox");
                tagChBL.InnerHtml += String.Format("<input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
                                                    "value=\"{1}\" {2} />{3}",
                                                    name,
                                                    item.Value,
                                                    selectedValues.Contains(item.Value) ? "checked=\"checked\"" : "",
                                                    item.Text);

                tagControlsContainer.InnerHtml += tagChBL.ToString();
            }

            
            tagMainContainer.InnerHtml = tagLabel.ToString(TagRenderMode.Normal) + tagControlsContainer.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(tagMainContainer.ToString(TagRenderMode.Normal));
        }
    }
}
