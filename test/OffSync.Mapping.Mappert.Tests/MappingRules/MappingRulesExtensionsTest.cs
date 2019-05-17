
using System;
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
            var mapper = new TestMapper(
                b =>
                {
                    b.Map(s => s.Name)
                        .To(t => t.Description);

                    b.Map(s => s.Values)
                        .To(t => t.Value1, t => t.Value2)
                        .Using(SplitToArray);

                    b.IgnoreSource(s => s.Ignored);

                    b.IgnoreTarget(t => t.Excluded);

                    b.Map(s => s.LookupValue)
                        .To(t => t.LookupId)
                        .Using(int.Parse);
                });

            var source = new SourceModel()
            {
                Values = "1,2",
                LookupValue = "3",
            };

            var target = mapper.Map(source);

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
            var mapper = new TestMapper(
                b =>
                {
                    b.Map(s => s.Name)
                        .To(t => t.Description);

                    b.Map(s => s.Values)
                        .To(t => t.Value1, t => t.Value2)
                        .Using(SplitToArray);

                    b.IgnoreSource(s => s.Ignored);

                    b.IgnoreTarget(t => t.Excluded);

                    b.Map(s => s.LookupValue)
                        .To(t => t.LookupId)
                        .Using(int.Parse);
                });

            var source = new SourceModel()
            {
                Values = "1,2,3",
            };

            Assert.That(
                () => mapper.Map(source),
                Throws.InvalidOperationException);
        }

        [Test]
        public void ApplyFromUnsupportedType()
        {
            Func<object, object> builder = o => new object();

            var mappingRule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Description)))
                .WithBuilder(builder);

            var source = new SourceModel()
            {
                Id = 1,
            };

            var target = new TargetModel();

            Assert.That(
                () => mappingRule.Apply(
                    source,
                    target),
                Throws.InvalidOperationException);
        }
    }
}