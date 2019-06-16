/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class MappingRulesUtilTest
    {
        public class CreateAutoMappingModel
        {
            public int Id { get; set; }

            public SourceNested[] ItemsList { get; set; }

            public int[] Numbers { get; set; }

            public IEnumerable<SourceNested> MoreItems { get; set; }
        }

        [Test]
        public void CreateAutoMappingShouldThrowExceptionOnMissingSourceProperty()
        {
            Assert.That(
                () => MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.Description))),
                Throws.ArgumentException);
        }

        [Test]
        public void CreateAutoMappingShouldWorkForValues()
        {
            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.Id)));
        }

        [Test]
        public void CreateAutoMappingShouldWorkForItems()
        {
            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsList)));

            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.Numbers)));

            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.MoreItems)));
        }
    }
}
