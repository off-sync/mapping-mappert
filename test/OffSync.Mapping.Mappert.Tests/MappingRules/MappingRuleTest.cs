using System;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class MappingRuleTest
    {
        private MappingRule sut;

        [SetUp]
        public void SetUp()
        {
            sut = new MappingRule();
        }

        [Test]
        public void ToStringShouldIncludeMappedProperties()
        {
            sut
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)));

            Assert.That(
                sut.ToString(),
                Is.EqualTo(@"[MapToValue: 'Values' -> 'Value1', 'Value2']"));
        }

        [Test]
        public void ToStringShouldIncludeBuilderIndication()
        {
            Func<string, (string, string)> builder = s => ("1", "2");

            sut
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithBuilder(builder);

            Assert.That(
                sut.ToString(),
                Is.EqualTo(@"[MapToValue*: 'Values' -> 'Value1', 'Value2']"));
        }


        [Test]
        public void ToStringShouldIncludeItemsTypes()
        {
            sut
                .WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), typeof(SourceNested))
                .WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)), typeof(TargetNested))
                .WithType(MappingRuleTypes.MapToArray);

            Assert.That(
                sut.ToString(),
                Is.EqualTo(@"[MapToArray: 'ItemsArray' -> 'ItemsArray' ('SourceNested' -> 'TargetNested')]"));
        }

        [Test]
        public void WithSourceItemsShouldCheckPreConditions()
        {
            Assert.That(
                () => sut.WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), null),
                Throws.ArgumentNullException);

            sut.WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), typeof(SourceNested));

            Assert.That(
                () => sut.WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), typeof(SourceNested)),
                Throws.InvalidOperationException);
        }

        [Test]
        public void WithSourceShouldCheckPreConditions()
        {
            Assert.That(
                () => sut.WithSource(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void WithTargetItemsShouldCheckPreConditions()
        {
            Assert.That(
                () => sut.WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)), null),
                Throws.ArgumentNullException);

            sut.WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)), typeof(TargetNested));

            Assert.That(
                () => sut.WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)), typeof(TargetNested)),
                Throws.InvalidOperationException);
        }

        [Test]
        public void WithTargetShouldCheckPreConditions()
        {
            Assert.That(
                () => sut.WithTarget(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void WithBuilderShouldCheckPreConditions()
        {
            Assert.That(
                () => sut.WithBuilder(null),
                Throws.ArgumentNullException);
        }
    }
}
