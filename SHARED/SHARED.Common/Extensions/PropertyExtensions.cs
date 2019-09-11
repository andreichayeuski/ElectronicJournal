using System;
using System.Linq.Expressions;

namespace SHARED.Common.Extensions
{
    public class PropertyExtensions
    {
        public class Nameof<T>
        {
            public static string Property<TProp>(Expression<Func<T, TProp>> expression)
            {
                var body = expression.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("'expression' should be a member expression");
                return body.Member.Name;
            }
        }

        public static MemberExpression GetMemberInfo(Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }
    }
}
