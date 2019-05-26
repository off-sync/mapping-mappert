using System;
using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class RemoveIgnoringMappingRulesValidatorTest
    {
        private RemoveIgnoringMappingRulesValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new RemoveIgnoringMappingRulesValidator();
        }

        [Test]
        public void ShouldReturnUpdateRulesWhenIgnoringRulesFound()
        {
            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id))),
            };

            var result = _sut.Validate<SourceModel, TargetModel>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.UpdateRules));

            Assert.That(
                result.RulesToRemove,
                Has.Exactly(1).Items);

            rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id))),
            };

            result = _sut.Validate<SourceModel, TargetModel>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.UpdateRules));

            Assert.That(
                result.RulesToRemove,
                Has.Exactly(1).Items);
        }

        [Test]
        public void ShouldReturnValidWhenNoIgnoringRulesFound()
        {
            var rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id))),
            };

            var result = _sut.Validate<SourceModel, TargetModel>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Valid));

            Func<int> builder = () => 1;

            rules = new List<MappingRule>()
            {
                new MappingRule()
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                    .WithBuilder(builder),
            };

            result = _sut.Validate<SourceModel, TargetModel>(rules);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleSetValidationResults.Valid));
        }
    }
}
