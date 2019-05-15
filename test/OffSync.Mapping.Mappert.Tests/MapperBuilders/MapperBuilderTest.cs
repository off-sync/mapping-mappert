using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Tests.Common;

namespace OffSync.Mapping.Mappert.Tests.MapperBuilders
{
    public class MapperBuilderTest
    {
        class TestMapperBuilder :
            MapperBuilder<SourceModel, TargetModel>
        {
            public TestMapperBuilder()
            {
                Map(s => s.Name)
                    .To(t => t.Description);
            }

            public IEnumerable<MappingRule> CheckedMappingRules => GetCheckedMappingRules();
        }


        private TestMapperBuilder _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TestMapperBuilder();
        }

        [Test]
        public void ChecksMappingRules()
        {
            var mappingRules = _sut.CheckedMappingRules;

            Assert.That(
                mappingRules,
                Has.Exactly(3).Items);
        }
    }
}