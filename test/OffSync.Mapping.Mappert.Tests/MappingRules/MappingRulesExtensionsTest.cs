
using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Tests.Common;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class MappingRulesExtensionsTest
    {
        [Test]
        public void ApplyFromArray()
        {
            Func<string, object[]> builder = SplitToArray;

            var sut = new MappingRule(null)
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithBuilder(builder);

            var source = new SourceModel()
            {
                Values = "1,2",
            };

            var target = new TargetModel();

            sut.Apply(
                source,
                target);

            Assert.That(
                target.Value1,
                Is.EqualTo("1"));

            Assert.That(
                target.Value2,
                Is.EqualTo("2"));
        }

        public object[] SplitToArray(string values)
        {
            return values
                .Split(',')
                .Cast<object>()
                .ToArray();
        }

        [Test]
        public void ApplyFromArrayChecksLength()
        {
            Func<string, object[]> builder = SplitToArray;

            var sut = new MappingRule(null)
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithBuilder(builder);

            var source = new SourceModel()
            {
                Values = "1,2,3",
            };

            var target = new TargetModel();

            Assert.That(
                () => sut.Apply(
                    source,
                    target),
                Throws.InvalidOperationException);
        }

        [Test]
        public void ApplyFromUnsupportedType()
        {
            Func<object, object> builder = o => new object();

            Assert.That(
                () => new MappingRule(null)
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Description)))
                    .WithBuilder(builder),
                Throws.ArgumentException);
        }

        [Test]
        public void ApplyToArray()
        {
            Func<SourceNested, TargetNested> builder = sn => new TargetNested() { Key = sn.Key, Value = sn.Value };

            var sut = new MappingRule(null)
                .WithSource(
                    typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsEnumerable)),
                    typeof(SourceNested))
                .WithTarget(
                    typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)),
                    typeof(TargetNested))
                .WithStrategy(MappingStrategies.MapToArray)
                .WithBuilder(builder);

            var source = new SourceModel()
            {
                ItemsEnumerable = new List<SourceNested>()
                {
                    new SourceNested()
                    {
                        Key = 1,
                        Value = "2",
                    },
                    new SourceNested()
                    {
                        Key = 3,
                        Value = "4",
                    },
                },
            };

            var target = new TargetModel();

            sut.Apply(
                source,
                target);

            Assert.That(
                target.ItemsArray[0].Key,
                Is.EqualTo(1));

            Assert.That(
                target.ItemsArray[0].Value,
                Is.EqualTo("2"));

            Assert.That(
                target.ItemsArray[1].Key,
                Is.EqualTo(3));

            Assert.That(
                target.ItemsArray[1].Value,
                Is.EqualTo("4"));
        }
    }
}