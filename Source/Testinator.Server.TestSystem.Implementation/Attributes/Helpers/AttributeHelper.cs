using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Exceptions;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    public static class AttributeHelper
    {
        internal static TValue GetPropertyAttributeValue<TIn, TAttribute, TValue>(
            Expression<Func<TIn, object>> propertyExpression,
            Func<TAttribute, TValue> valueSelector,
            int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            var propertyInfo = ExpressionHelpers.GetPropertyInfo(propertyExpression);
            var filtered = 
                 Filter<TAttribute>(propertyInfo, currentVersion)
                .Select(x => new
                {
                    x.Key,
                    Values = x.Select(attr => valueSelector(attr))
                })
                .FirstOrDefault();

            if (filtered == null)
                return default;

            if (filtered.Values.Count() > 1)
                throw new VersioningAmbiguityException(typeof(TAttribute).ToString());

            return filtered.Values.FirstOrDefault();
        }

        internal static TValue GetPropertyAttributeValue<TIn, TOut, TAttribute, TValue>(
            Expression<Func<TIn, TOut>> propertyExpression,
            Func<TAttribute, TValue> valueSelector,
            int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            var expression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)expression.Member;
            var filtered =
                Filter<TAttribute>(propertyInfo, currentVersion)
                .Select(x => new
                {
                    x.Key,
                    Values = x.Select(attr => valueSelector(attr))
                })
                .FirstOrDefault();

            if (filtered == null)
                return default;

            if (filtered.Values.Count() > 1)
                throw new VersioningAmbiguityException(typeof(TAttribute).ToString());

            return filtered.Values.FirstOrDefault();
        }

        internal static TAttribute GetPropertyAttribute<TIn, TOut, TAttribute>(
            Expression<Func<TIn, TOut>> propertyExpression,
            int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            var expression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)expression.Member;
            var filtered = 
                Filter<TAttribute>(propertyInfo, currentVersion)
                .Select(x => new
                {
                    x.Key,
                    Values = x.Select(attr => attr)
                })
                .FirstOrDefault();

            if (filtered == null)
                return default;

            if (filtered.Values.Count() > 1)
                throw new VersioningAmbiguityException(typeof(TAttribute).ToString());

            return filtered.Values.FirstOrDefault();
        }

        private static IOrderedEnumerable<IGrouping<int, TAttribute>> Filter<TAttribute>(PropertyInfo propertyInfo, int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            return propertyInfo
                .GetCustomAttributes(typeof(TAttribute), true)
                .Cast<TAttribute>()
                .Where(attr => attr.FromVersion <= currentVersion)
                .GroupBy(attr => attr.FromVersion)
                .OrderByDescending(x => x.Key);
        }
    }
}
