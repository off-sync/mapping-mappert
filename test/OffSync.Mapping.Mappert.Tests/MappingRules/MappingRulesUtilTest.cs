using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Tests.Common;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation.Exceptions;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class MappingRulesUtilTest
    {
        [Test]
        public void EnsureNoDuplicateTargetMappings()
        {
            var builder = new TestMapper(
                b =>
                {
                    b.Map(s => s.Id)
                        .To(t => t.Id);

                    b.Map(s => s.Name)
                        .To(t => t.Id);

                    b.IgnoreSource(s => s.Ignored);

                    b.IgnoreTarget(t => t.Excluded);

                    b.Map(s => s.LookupValue)
                        .To(t => t.LookupId)
                        .Using(int.Parse);

                    b.MapItems(s => s.ItemsEnumerable)
                        .To(t => t.ItemsArray);

                    b.MapItems(s => s.ItemsArray)
                        .To(t => t.ItemsCollection);

                    b.MapItems(s => s.ItemsArray)
                        .To(t => t.ItemsList);

                    b.IgnoreTarget(t => t.NumbersCollection);

                    b.MapItems(s => s.Numbers)
                        .To(t => t.NumbersList);

                    b.Map(s => s.Nested)
                        .To(t => t.NestedToo);
                });

            Assert.That(
                () => builder.Map(null),
                Throws.TypeOf<MappingRuleSetValidationException>());
        }

        [Test]
        public void EnsureAllTargetPropertiesMapped()
        {
            var builder = new TestMapper(
                b =>
                {
                    // target property 'Value2' is not mapped

                    b.Map(s => s.Name)
                        .To(t => t.Description);

                    b.Map(s => s.Values)
                        .To(t => t.Value1);

                    b.IgnoreSource(s => s.Ignored);

                    b.IgnoreTarget(t => t.Excluded);

                    b.Map(s => s.LookupValue)
                        .To(t => t.LookupId)
                        .Using(int.Parse);

                    b.MapItems(s => s.ItemsEnumerable)
                        .To(t => t.ItemsArray);

                    b.MapItems(s => s.ItemsArray)
                        .To(t => t.ItemsCollection);

                    b.MapItems(s => s.ItemsArray)
                        .To(t => t.ItemsList);

                    b.IgnoreTarget(t => t.NumbersCollection);

                    b.MapItems(s => s.Numbers)
                        .To(t => t.NumbersList);

                    b.Map(s => s.Nested)
                        .To(t => t.NestedToo);
                });

            Assert.That(
                () => builder.Map(null),
                Throws.TypeOf<MappingRuleSetValidationException>());
        }

        [Test]
        public void EnsureAllSourcePropertiesMapped()
        {
            var builder = new TestMapper(
                b =>
                {
                    // source property 'Name' is not mapped

                    b.Map(s => s.Id)
                        .To(t => t.Description)
                        .Using(i => i.ToString());

                    b.Map(s => s.Values)
                        .To(t => t.Value1, t => t.Value2)
                        .Using(v => v.Split(','));

                    b.IgnoreSource(s => s.Ignored);

                    b.IgnoreTarget(t => t.Excluded);

                    b.Map(s => s.LookupValue)
                        .To(t => t.LookupId)
                        .Using(int.Parse);

                    b.MapItems(s => s.ItemsEnumerable)
                        .To(t => t.ItemsArray);

                    b.MapItems(s => s.ItemsArray)
                        .To(t => t.ItemsCollection);

                    b.MapItems(s => s.ItemsArray)
                        .To(t => t.ItemsList);

                    b.IgnoreTarget(t => t.NumbersCollection);

                    b.MapItems(s => s.Numbers)
                        .To(t => t.NumbersList);

                    b.Map(s => s.Nested)
                        .To(t => t.NestedToo);
                });

            Assert.That(
                () => builder.Map(null),
                Throws.TypeOf<MappingRuleSetValidationException>());
        }

        [Test]
        public void EnsureValidBuildersCreatesAutoMappingsForMissingBuilders()
        {
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Nested)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Nested)));

            var checkedRule = MappingRulesUtil
                .WithAutoMappingBuilders(rule.Yield())
                .Single();

            Assert.That(
                checkedRule.Builder,
                Is.Not.Null);

            // should throw exception if auto-mapping is not possible
            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Description)));

            Assert.That(
                () => MappingRulesUtil.WithAutoMappingBuilders(rule.Yield()),
                Throws.InvalidOperationException);

            // should throw exception if auto-mapping is not possible for items
            rule = new MappingRule()
                .WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), typeof(SourceNested))
                .WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.Numbers)), typeof(int))
                .WithType(MappingRuleTypes.MapToArray);

            Assert.That(
                () => MappingRulesUtil.WithAutoMappingBuilders(rule.Yield()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void EnsureValidBuildersChecksBuildersForMultiPropertyMappings()
        {
            // multiple target properties require a builder
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)));

            Assert.That(
                () => MappingRulesUtil.EnsureValidBuilders(rule.Yield()),
                Throws.InvalidOperationException);

            // multiple source properties require a builder
            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Name)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)));

            Assert.That(
                () => MappingRulesUtil.EnsureValidBuilders(rule.Yield()),
                Throws.InvalidOperationException);

            // multiple source and target properties require a builder
            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Name)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)));

            Assert.That(
                () => MappingRulesUtil.EnsureValidBuilders(rule.Yield()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void EnsureValidBuildersChecksBuildersForTargetPropertyOnlyMappings()
        {
            // multiple target properties require a builder
            var rule = new MappingRule()
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)));

            Assert.That(
                () => MappingRulesUtil.EnsureValidBuilders(rule.Yield()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void EnsureValidItemsMappings()
        {
            // check for single source properties
            var rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Name)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithType(MappingRuleTypes.MapToArray);

            Assert.That(
                () => MappingRulesUtil.EnsureValidItemsMappings(rule.Yield()),
                Throws.InvalidOperationException);

            // check for single target properties
            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithType(MappingRuleTypes.MapToArray);

            Assert.That(
                () => MappingRulesUtil.EnsureValidItemsMappings(rule.Yield()),
                Throws.InvalidOperationException);

            // check for source property items type
            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithType(MappingRuleTypes.MapToArray);

            Assert.That(
                () => MappingRulesUtil.EnsureValidItemsMappings(rule.Yield()),
                Throws.InvalidOperationException);

            // check for target property items type
            rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithType(MappingRuleTypes.MapToArray);

            Assert.That(
                () => MappingRulesUtil.EnsureValidItemsMappings(rule.Yield()),
                Throws.InvalidOperationException);
        }

        public class CreateAutoMappingModel
        {
            public SourceNested[] ItemsList { get; set; }

            public int[] Numbers { get; set; }

            public IEnumerable<SourceNested> MoreItems { get; set; }
        }

        [Test]
        public void CreateAutoMapping()
        {
            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsList)));

            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.Numbers)));

            MappingRulesUtil.CreateAutoMapping<CreateAutoMappingModel>(
                typeof(TargetModel).GetProperty(nameof(TargetModel.MoreItems)));
        }
    }
}
