using NUnit.Framework;

using OffSync.Mapping.Mappert.Tests.Common;

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
                });

            Assert.That(
                () => builder.CheckedMappingRules,
                Throws.InvalidOperationException);
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
                });

            Assert.That(
                () => builder.CheckedMappingRules,
                Throws.InvalidOperationException);
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
                });

            Assert.That(
                () => builder.CheckedMappingRules,
                Throws.InvalidOperationException);
        }

        [Test]
        public void EnsureValidBuilders()
        {
            var builder = new TestMapper(
                b =>
                {
                    // 'Values' -> 'Value1', 'Value2' mapping is missing a builder

                    b.Map(s => s.Name)
                        .To(t => t.Description)
                        .Using(i => i.ToString());

                    b.Map(s => s.Values)
                        .To(t => t.Value1, t => t.Value2);

                    b.IgnoreSource(s => s.Ignored);

                    b.IgnoreTarget(t => t.Excluded);
                });

            Assert.That(
                () => builder.CheckedMappingRules,
                Throws.InvalidOperationException);
        }
    }
}
