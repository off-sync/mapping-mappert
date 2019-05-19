
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Reflection.MappingSteps;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class MappingStepsExtensionsTest
    {
        [Test]
        public void ApplyFromArray()
        {
            Func<string, object[]> builder = SplitToArray;

            var sut = new MappingStep()
            {
                SourceProperties = new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Values)) },
                TargetProperties = new PropertyInfo[]
                {
                    typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)),
                    typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)),
                },
                Builder = builder,
                BuilderType = BuilderTypes.ObjectArray,
            };

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

            var sut = new MappingStep()
            {
                SourceProperties = new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Values)) },
                TargetProperties = new PropertyInfo[]
                {
                    typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)),
                    typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)),
                },
                Builder = builder,
                BuilderType = BuilderTypes.ObjectArray,
            };

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
        public void ApplyToArray()
        {
            Func<SourceNested, TargetNested> builder = sn => new TargetNested() { Key = sn.Key, Value = sn.Value };

            var sut = new MappingStep()
            {
                SourceProperties = new PropertyInfo[] {
                    typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsEnumerable)),
                },
                TargetProperties = new PropertyInfo[]
                {
                    typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)),
                },
                TargetItemType = typeof(TargetNested),
                MappingRuleType = MappingRuleTypes.MapToArray,
                Builder = builder,
                BuilderType = BuilderTypes.ObjectArray,
            };

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