using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Helpers for dealing with expressions
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Get the name of the property the expression is pointing at
        /// </summary>
        /// <typeparam name="T">The object that contains that property</typeparam>
        /// <param name="expression">The expression that should be pointing at a property</param>
        /// <exception cref="NotSupportedException">If the expression doesn't point at a property</exception>
        /// <returns>The name of the property</returns>
        public static string GetCorrectPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            var memberInfo = expression.GetMemberInfo();
            return memberInfo.MemberType == MemberTypes.Property 
                ? ((PropertyInfo)memberInfo).Name : throw new NotSupportedException("Expression doesn't point to a property.");
        }

        /// <summary>
        /// Get the property info of the property the expression is pointing at
        /// </summary>
        /// <typeparam name="T">The object that contains that property</typeparam>
        /// <param name="expression">The expression that should be pointing at a property</param>
        /// <exception cref="NotSupportedException">If the expression doesn't point at a property</exception>
        /// <returns>Property info</returns>
        public static PropertyInfo GetPropertyInfo<T> (this Expression<Func<T, object>> expression)
        {
            var memberInfo = expression.GetMemberInfo();
            return memberInfo.MemberType == MemberTypes.Property 
                ? (PropertyInfo)memberInfo : throw new NotSupportedException("Expression doesn't point to a property.");
        }

        /// <summary>
        /// Gets member info from the expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression</param>
        /// <exception cref="NotSupportedException">If the expression doesn't point at a property</exception>
        /// <returns>Info about the member</returns>
        private static MemberInfo GetMemberInfo<T> (this Expression<Func<T, object>> expression)
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
        public static Type GetObjectType<T>(this Expression<Func<T, object>> expr)
        {
            if ((expr.Body.NodeType == ExpressionType.Convert) ||
                (expr.Body.NodeType == ExpressionType.ConvertChecked))
            {
                if (expr.Body is UnaryExpression unary)
                    return unary.Operand.Type;
            }
            return expr.Body.Type;
        }
    }
}
