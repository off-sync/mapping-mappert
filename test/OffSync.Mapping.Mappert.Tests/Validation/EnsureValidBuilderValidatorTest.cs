using System;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class EnsureValidBuilderValidatorTest
    {
        private EnsureValidBuilderValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EnsureValidBuilderValidator();
        }

        [Test]
        public void ShouldReturnInvalidWhenBuilderMissing()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)));

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));

            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)));

            result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));

            rule = new MappingRule()
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)));

            result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnInvalidWhenBuilderIsInvalid()
        {
            Func<int, string> builder = i => i.ToString();

            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithBuilder(builder);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnValidWhenBuilderIsValid()
        {
            Func<int, int> builder = i => i;

            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithBuilder(builder);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Valid));
        }
    }
}
