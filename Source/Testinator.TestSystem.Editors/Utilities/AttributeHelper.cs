﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    internal static class AttributeHelper
    {
        internal static TValue GetPropertyAttributeValue<TIn, TAttribute, TValue>(
            Expression<Func<TIn, object>> propertyExpression,
            Func<TAttribute, TValue> valueSelector,
            int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            var propertyInfo = propertyExpression.GetPropertyInfo();
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

        internal static TAttribute GetPropertyAttribute<TIn, TAttribute>(
            Expression<Func<TIn, object>> propertyExpression,
            int currentVersion)
            where TAttribute : BaseEditorAttribute
        {
            var propertyInfo = propertyExpression.GetPropertyInfo();
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
