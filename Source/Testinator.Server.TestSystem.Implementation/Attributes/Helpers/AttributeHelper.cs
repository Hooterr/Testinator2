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
        internal static TValue GetPropertyAttributeValue<TIn, TOut, TAttribute, TValue>(
            Expression<Func<TIn, TOut>> propertyExpression,
            Func<TAttribute, TValue> valueSelector,
            int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            var expression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)expression.Member;
            var filtered = propertyInfo
                .GetCustomAttributes(typeof(TAttribute), true)
                .Cast<TAttribute>()
                .Where(attr => attr.FromVersion <= currentVersion)
                .GroupBy(attr => attr.FromVersion)
                .OrderByDescending(x => x.Key)
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
    }
}
