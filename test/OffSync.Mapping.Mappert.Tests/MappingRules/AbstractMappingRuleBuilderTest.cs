using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class AbstractMappingRuleBuilderTest
    {
        public class MappingRuleBuilder :
            AbstractMappingRuleBuilder
        {
            public MappingRuleBuilder(MappingRule mappingRule) :
                base(mappingRule)
            {
            }
        }

        [Test]
        public void ConstructorShouldCheckPreConditions()
        {
            Assert.That(
                () => new MappingRuleBuilder(null),
                Throws.ArgumentNullException);
        }
    }
}
