using System;
using System.Linq.Expressions;
using System.Reflection;

namespace OffSync.Mapping.Mappert.Common
{
    public static class ExpressionsUtil
    {
        public static PropertyInfo GetPropertyFromExpression(
            LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException(
                    $"expression body must by of type {nameof(MemberExpression)}",
                    nameof(expression));
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    $"expression body must access a property",
                    nameof(expression));
            }

            return propertyInfo;
        }
    }
}
