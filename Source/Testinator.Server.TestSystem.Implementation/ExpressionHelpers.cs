using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class ExpressionHelpers
    {
        public static string GetCorrectPropertyName<T>(Expression<Func<T, object>> expression)
        {
            MemberInfo memberInfo = null;
            if (expression.Body is MemberExpression)
            {
                memberInfo = ((MemberExpression)expression.Body).Member;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                memberInfo = ((MemberExpression)op).Member;
            }

            return memberInfo.MemberType == MemberTypes.Property ? ((PropertyInfo)memberInfo).Name : string.Empty;
        }

        public static PropertyInfo GetPropertyInfo<T> (Expression<Func<T, object>> expression)
        {
            MemberInfo memberInfo = null;

            if (expression.Body is MemberExpression)
            {
                memberInfo = ((MemberExpression)expression.Body).Member;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                memberInfo = ((MemberExpression)op).Member;
            }

            return memberInfo.MemberType == MemberTypes.Property ? (PropertyInfo)memberInfo : null;
        }
    }
}
