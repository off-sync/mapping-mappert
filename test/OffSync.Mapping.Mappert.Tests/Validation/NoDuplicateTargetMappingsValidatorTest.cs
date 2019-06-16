/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class NoDuplicateTargetMappingsValidatorTest
    {
        private NoDuplicateTargetMappingsValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new NoDuplicateTargetMappingsValidator();
        }

        [Test]
        public void ShouldReturnInvalidWhenDuplicateTargetMappingsFound()
        {
            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id))),
                new MappingRule()
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id))),
            };

            var result = _sut.Validate<SourceModel, TargetModel>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnValidWhenNoDuplicateTargetMappingsFound()
        {
            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id))),
            };

            var result = _sut.Validate<SourceModel, TargetModel>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Valid));
        }
    }
}
