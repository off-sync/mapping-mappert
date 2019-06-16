/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class EnsureValidItemsMappingValidatorTest
    {
        private EnsureValidItemsMappingValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EnsureValidItemsMappingValidator();
        }

        [Test]
        public void ShouldReturnValidForNonItemsMappings()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)));

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Valid));
        }

        [Test]
        public void ShouldReturnInvalidForMultipleSourceProperties()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithType(MappingRuleTypes.MapToArray);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnInvalidForMultipleTargetProperties()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithType(MappingRuleTypes.MapToCollection);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnInvalidWhenSourceItemsTypeCannotBeDetermined()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTargetItems(
                    typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)),
                    typeof(TargetNested))
                .WithType(MappingRuleTypes.MapToArray);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnInvalidWhenTargetItemsTypeCannotBeDetermined()
        {
            var rule = new MappingRule()
                .WithSourceItems(
                    typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)),
                    typeof(SourceNested))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithType(MappingRuleTypes.MapToArray);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Invalid));
        }

        [Test]
        public void ShouldReturnValid()
        {
            var rule = new MappingRule()
                .WithSourceItems(
                    typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)),
                    typeof(SourceNested))
                .WithTargetItems(
                    typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)),
                    typeof(TargetNested))
                .WithType(MappingRuleTypes.MapToArray);

            var result = _sut.Validate<SourceModel, TargetModel>(rule);

            Assert.That(
                result.Result,
                Is.EqualTo(MappingRuleValidationResults.Valid));
        }
    }
}
