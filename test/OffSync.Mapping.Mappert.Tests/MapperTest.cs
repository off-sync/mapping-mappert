using NUnit.Framework;

using OffSync.Mapping.Mappert.Tests.Common;

namespace OffSync.Mapping.Mappert.Tests
{
    [TestFixture]
    public class MapperTest
    {
        private TestMapper _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TestMapper();
        }

        [Test]
        public void Maps()
        {
            var source = new SourceModel()
            {
                Id = 1,
                Name = "2",
                Nested = new SourceNested()
                {
                    Key = 3,
                    Value = "4",
                },
            };

            var target = _sut.Map(source);

            Assert.That(
                target.Id,
                Is.EqualTo(1));

            Assert.That(
                target.Description,
                Is.EqualTo("2"));

            Assert.That(
                target.Nested.Key,
                Is.EqualTo(3));

            Assert.That(
                target.Nested.Value,
                Is.EqualTo("4"));
        }
    }
}
