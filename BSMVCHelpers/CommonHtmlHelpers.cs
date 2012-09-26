using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Ovixon.Common
{
    internal static class CommonHtmlHelpers
    {
        internal static string FieldDisplayName(this HtmlHelper htmlh, string name)
        {
            var metadata = ModelMetadata.FromStringExpression(name, htmlh.ViewData);
            return metadata.DisplayName;
        }

        internal static string FieldValue(this HtmlHelper htmlh, string name)
        {
            var metadata = ModelMetadata.FromStringExpression(name, htmlh.ViewData);
            return metadata.SimpleDisplayText;
        }

        internal static TProperty GetValue<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            TModel model = htmlHelper.ViewData.Model;
            if (model == null)
            {
                return default(TProperty);
            }
            Func<TModel, TProperty> func = expression.Compile();
            return func(model);
        }
    }

    internal enum StateElement
    {
        Get,
        PostWithError,
        PostWithOutError
    }
}