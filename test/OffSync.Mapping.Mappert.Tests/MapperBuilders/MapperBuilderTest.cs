using NUnit.Framework;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.MapperBuilders
{
    [TestFixture]
    public class MapperBuilderTest
    {
        [Test]
        public void ConstructorShouldCheckPreConditions()
        {
            Assert.That(
                () => new MapperBuilder<SourceModel, TargetModel>(null),
                Throws.ArgumentNullException);
        }
    }
}
