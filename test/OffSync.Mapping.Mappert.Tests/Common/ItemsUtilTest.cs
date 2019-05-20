using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    [TestFixture]
    public class ItemsUtilTest
    {
        [Test]
        [TestCase(typeof(string[]))]
        [TestCase(typeof(IEnumerable<string>))]
        [TestCase(typeof(ICollection<string>))]
        [TestCase(typeof(IReadOnlyCollection<string>))]
        [TestCase(typeof(IList<string>))]
        [TestCase(typeof(IReadOnlyList<string>))]
        [TestCase(typeof(List<string>))]
        public void TryGetTargetItemsTypeReturnsSupportedItemsType(
            Type targetPropertyType)
        {
            Assert.That(
                ItemsUtil.TryGetTargetItemsType(
                    targetPropertyType,
                    out var itemsType),
               Is.True);

            Assert.That(
                itemsType,
                Is.EqualTo(typeof(string)));
        }

        [Test]
        public void TryGetTargetItemsTypeReturnsFalseIfItemsTypeNotSupported()
        {
            Assert.That(
                ItemsUtil.TryGetTargetItemsType(
                    typeof(string),
                    out var itemsType),
               Is.False);

            Assert.That(
                itemsType,
                Is.Null);

            Assert.That(
                ItemsUtil.TryGetTargetItemsType(
                    typeof(IList),
                    out itemsType),
               Is.False);
        }
    }
}
