/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Reflection.Common;

namespace OffSync.Mapping.Mappert.Reflection.Tests.Common
{
    [TestFixture]
    public class ItemsUtilTest
    {
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
