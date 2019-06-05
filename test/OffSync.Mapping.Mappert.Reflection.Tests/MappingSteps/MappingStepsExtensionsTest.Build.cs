using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Reflection.MappingSteps;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Reflection.Tests.MappingRules
{
    [TestFixture]
    public partial class MappingStepsExtensionsTest
    {
        [Test]
        public void BuildShouldUseBuildersCacheIfAvailable()
        {
            var nestedMapper = new Mapper<SourceNested, TargetNested>();

            Func<SourceNested, TargetNested> builder = nestedMapper.Map;

            var sut = new MappingStep()
            {
                SourceProperties =
                    new PropertyInfo[]
                    {
                        typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsEnumerable))
                    },
                TargetProperties =
                    new PropertyInfo[]
                    {
                        typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray))
                    },
                Builder = builder,
                BuilderInvoke = GetBuilderInvoke(builder),
                BuilderType = BuilderTypes.SingleValue,
                MappingRuleType = MappingRuleTypes.MapToArray,
                TargetItemType = typeof(TargetNested),
            };

            var sourceNested = new SourceNested()
            {
                Key = 1,
                Value = "2",
            };

            var source = new SourceModel()
            {
                ItemsEnumerable = Enumerable
                    .Range(0, 2)
                    .Select(_ => sourceNested),
            };

            var target = new TargetModel();

            sut.Apply(
                source,
                target);

            Assert.That(
                target.ItemsArray,
                Has.Exactly(2).Items);

            Assert.That(
                target.ItemsArray[0],
                Is.SameAs(target.ItemsArray[1]));
        }
    }
}