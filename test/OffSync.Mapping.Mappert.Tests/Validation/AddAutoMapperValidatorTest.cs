using System;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class AddAutoMapperValidatorTest
    {
        private AddAutoMapperValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddAutoMapperValidator();
        }

        [Test]
        public void ShouldCheckPreConditions()
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
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)));

            result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));

            rule = new MappingRule()
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)));

            result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnValidWhenBuilderIsPresent()
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

        [Test]
        public void ShouldReturnValidWhenTargetPropertiesEmpty()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)));

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Valid));
        }

        [Test]
        public void ShouldReturnSetBuilder()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Nested)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Nested)));

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.SetBuilder));

            Assert.That(
                result.Builder,
                Is.Not.Null);
        }

        [Test]
        public void ShouldReturnInvalidIfAutoMapperFailed()
        {
            // should return invalid if auto-mapping is not possible
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Description)));

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));

            // should return invalid if auto-mapping is not possible for items
            rule = new MappingRule()
                .WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), typeof(SourceNested))
                .WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.Numbers)), typeof(int))
                .WithType(MappingRuleTypes.MapToArray);

            result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }
    }
}
