using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression AccessNextNestedMemberWithCheckForNull(this Expression @this, string memberPart, string otherPart)
        {
            if (string.IsNullOrEmpty(memberPart))
            {
                return @this;
            }

            Expression member = Expression.PropertyOrField(@this, memberPart);
            var nextPart = AccessNextNestedMemberWithCheckForNull(member, otherPart,
                (otherPart != null && otherPart.Contains(".")) ? otherPart.Before(".") : null);
            return Expression.Condition(
                Expression.NotEqual(member,
                    Expression.Constant(null)),
                nextPart,
                Expression.Constant(null, nextPart.Type)
            );
        }
    }
}
