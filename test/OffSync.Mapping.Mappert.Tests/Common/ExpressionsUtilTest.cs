/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq.Expressions;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    [TestFixture]
    public class ExpressionsUtilTest
    {
        public int TestField;

        [Test]
        public void GetPropertyFromExpressionShouldCheckPreConditions()
        {
            Assert.That(
                () => ExpressionsUtil.GetPropertyFromExpression(null),
                Throws.ArgumentNullException);

            Expression<Func<object, string>> constantExpression = (o) => "a";

            Assert.That(
                () => ExpressionsUtil.GetPropertyFromExpression(constantExpression),
                Throws.ArgumentException);

            Expression<Func<ExpressionsUtilTest, int>> methodExpression = (t) => t.TestField;

            Assert.That(
                () => ExpressionsUtil.GetPropertyFromExpression(methodExpression),
                Throws.ArgumentException);
        }
    }
}
