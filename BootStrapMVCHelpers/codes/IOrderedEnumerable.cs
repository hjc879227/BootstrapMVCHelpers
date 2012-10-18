using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Ovixon.WebSite.codes
{
    public static class IOrderedEnumerable
    {
        public static IOrderedEnumerable<TSource>
            OrderByWithDirection<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, bool descending)
        {
            return descending ?
                source.OrderByDescending(keySelector) :
                source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByWithDirection<TSource,
            TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector, bool descending)
        {
            return descending ?
                source.OrderByDescending(keySelector) :
                source.OrderBy(keySelector);
        }

        public static object Parse(this string s)
        {
            int i;
            DateTime d;
            if (int.TryParse(s, out i))
                return i;
            if (DateTime.TryParse(s, out d))
                return d;
            return s;
        }
    }
}