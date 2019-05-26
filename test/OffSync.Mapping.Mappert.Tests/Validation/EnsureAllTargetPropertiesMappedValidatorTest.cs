using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class EnsureAllTargetPropertiesMappedValidatorTest
    {
        public class Source
        {
        }

        public class Target
        {
            public int Id { get; set; }
        }

        private EnsureAllTargetPropertiesMappedValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EnsureAllTargetPropertiesMappedValidator();
        }

        [Test]
        public void ShouldReturnInvalidWhenNotAllTargetPropertiesMatched()
        {
            var result = _sut.Validate<Source, Target>(
                Enumerable.Empty<MappingRule>());

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnValidWhenAllTargetPropertiesMatched()
        {
            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Id))),
            };

            var result = _sut.Validate<Source, Target>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Valid));
        }
    }
}
