/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class AddAutoMappingValidatorTest
    {
        public class Source
        {
            public int Compatible { get; set; }

            public int Incompatible { get; set; }
        }

        public class Target
        {
            public int Compatible { get; set; }

            public string Incompatible { get; set; }
        }

        private AddAutoMappingValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddAutoMappingValidator();
        }

        [Test]
        public void ShouldReturnValidWhenAllPropertiesMapped()
        {
            Func<int, string> builder = i => i.ToString();

            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Compatible)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Compatible))),
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Incompatible)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Incompatible)))
                    .WithBuilder(builder),
            };

            var result = _sut.Validate<Source, Target>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Valid));
        }

        [Test]
        public void ShouldReturnUpdateRulesWhenAutoMappingCanBeAdded()
        {
            Func<int, string> builder = i => i.ToString();

            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Incompatible)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Incompatible)))
                    .WithBuilder(builder),
            };

            var result = _sut.Validate<Source, Target>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.UpdateRules));

            Assert.That(
                result.RulesToAdd,
                Has.Exactly(1).Items);
        }

        [Test]
        public void ShouldReturnInvalidWhenAutoMappingCannotBeAdded()
        {
            Func<int, string> builder = i => i.ToString();

            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Compatible)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Compatible)))
                    .WithBuilder(builder),
            };

            var result = _sut.Validate<Source, Target>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Invalid));
        }
    }
}
