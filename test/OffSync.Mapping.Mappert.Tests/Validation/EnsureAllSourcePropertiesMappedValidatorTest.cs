/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class EnsureAllSourcePropertiesMappedValidatorTest
    {
        public class Source
        {
            public int Id { get; set; }
        }

        public class Target
        {
        }

        private EnsureAllSourcePropertiesMappedValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EnsureAllSourcePropertiesMappedValidator();
        }

        [Test]
        public void ShouldReturnInvalidWhenNotAllSourcePropertiesMatched()
        {
            var result = _sut.Validate<Source, Target>(
                Enumerable.Empty<MappingRule>());

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnValidWhenAllSourcePropertiesMatched()
        {
            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Id))),
            };

            var result = _sut.Validate<Source, Target>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Valid));
        }
    }
}
