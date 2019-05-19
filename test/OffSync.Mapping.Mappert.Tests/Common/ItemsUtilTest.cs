using System.Collections.Generic;

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
    }
}
