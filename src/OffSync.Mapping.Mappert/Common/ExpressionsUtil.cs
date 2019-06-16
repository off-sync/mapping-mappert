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
            #region Pre-conditions
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException(
                    Messages.ExpressionBodyMustByOfTypeMemberExpression,
                    nameof(expression));
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    Messages.ExpressionBodyMustAccessProperty,
                    nameof(expression));
            }
            #endregion

            return propertyInfo;
        }
    }
}
