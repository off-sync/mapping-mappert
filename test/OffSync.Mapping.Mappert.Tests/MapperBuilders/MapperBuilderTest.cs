
using NUnit.Framework;

using OffSync.Mapping.Mappert.Tests.Common;

namespace OffSync.Mapping.Mappert.Tests.MapperBuilders
{
    public class MapperBuilderTest
    {
        private TestMapper _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TestMapper();
        }

        [Test]
        public void ChecksMappingRules()
        {
            var mappingRules = _sut.CheckedMappingRules;

            Assert.That(
                mappingRules,
                Has.Exactly(5).Items);

            mappingRules = _sut.CheckedMappingRules;

            Assert.That(
                mappingRules,
                Has.Exactly(5).Items);
        }
    }
}