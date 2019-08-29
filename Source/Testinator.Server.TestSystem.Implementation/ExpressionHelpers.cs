using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class ExpressionHelpers
    {
        public static string GetCorrectPropertyName<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                return ((MemberExpression)op).Member.Name;
            }
        }
    }
}
