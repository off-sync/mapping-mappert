using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    [TestFixture]
    public class ItemsUtilTest
    {
        [Test]
        public void TryGetTargetItemsType()
        {
            Assert.That(
                ItemsUtil.TryGetTargetItemsType(
                    typeof(ICollection<string>),
                    out var itemsType),
               Is.True);

            Assert.That(
                itemsType,
                Is.EqualTo(typeof(string)));

            Assert.That(
                ItemsUtil.TryGetTargetItemsType(
                    typeof(string),
                    out itemsType),
               Is.False);

            Assert.That(
                itemsType,
                Is.Null);
        }

        [Test]
        public void ItemsCountSupportsArrays()
        {
            Assert.That(
                ItemsUtil.GetItemsCount(new int[] { 1, 2, 3 }),
                Is.EqualTo(3));
        }

        [Test]
        public void ItemsCountSupportsCollections()
        {
            Assert.That(
                ItemsUtil.GetItemsCount(new List<int> { 1, 2, 3 }.AsReadOnly()),
                Is.EqualTo(3));
        }

        [Test]
        public void ItemsCountSupportsEnumerables()
        {
            Assert.That(
                ItemsUtil.GetItemsCount(Enumerable.Range(1, 3)),
                Is.EqualTo(3));
        }

        [Test]
        public void ItemsCountThrowsExceptionOnUnsupportedType()
        {
            Assert.That(
                () => ItemsUtil.GetItemsCount(123),
                Throws.ArgumentException);
        }
    }
}
