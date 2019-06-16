/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.DynamicMethods.Common;

namespace OffSync.Mapping.Mappert.DynamicMethods.Tests.Common
{
    [TestFixture]
    public class ItemsUtilTest
    {
        [Test]
        public void GetItemsCountShouldSupportArrays()
        {
            Assert.That(
                ItemsUtil.GetItemsCount<int>(new int[] { 1, 2, 3 }),
                Is.EqualTo(3));
        }

        [Test]
        public void GetItemsCountShouldSupportCollections()
        {
            Assert.That(
                ItemsUtil.GetItemsCount<int>(new List<int>() { 1, 2, 3 }),
                Is.EqualTo(3));
        }

        [Test]
        public void GetItemsCountShouldSupportEnumerables()
        {
            Assert.That(
                ItemsUtil.GetItemsCount<int>(Enumerable.Range(1, 3)),
                Is.EqualTo(3));
        }

        [Test]
        public void GetItemsCountShouldThrowExceptionOnUnsupportedType()
        {
            Assert.That(
                () => ItemsUtil.GetItemsCount<int>("123"),
                Throws.ArgumentException);
        }
    }
}
