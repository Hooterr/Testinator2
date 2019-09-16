using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Testinator.TestSystem.Editors
{
    public static class ExpressionHelpers
    {
        public static string GetCorrectPropertyName<T>(Expression<Func<T, object>> expression)
        {
            var memberInfo = GetMemberInfoFromExpression(expression);
            return memberInfo.MemberType == MemberTypes.Property 
                ? ((PropertyInfo)memberInfo).Name : throw new NotSupportedException("Expression doesn't point to a property.");
        }

        public static PropertyInfo GetPropertyInfo<T> (Expression<Func<T, object>> expression)
        {
            var memberInfo = GetMemberInfoFromExpression(expression);
            return memberInfo.MemberType == MemberTypes.Property 
                ? (PropertyInfo)memberInfo : throw new NotSupportedException("Expression doesn't point to a property.");
        }

        private static MemberInfo GetMemberInfoFromExpression<T> (Expression<Func<T,object>> expression)
        {
            MemberInfo memberInfo = null;
            try
            {
                if (expression.Body is MemberExpression)
                {
                    memberInfo = ((MemberExpression)expression.Body).Member;
                }
                else
                {
                    var op = ((UnaryExpression)expression.Body).Operand;
                    memberInfo = ((MemberExpression)op).Member;
                }
            }
            catch (Exception ex)
            {
                throw new NotSupportedException("Expression doesn't point to a property.", ex);
            }

            return memberInfo;
        }
    }
}
