using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.DynamicMethods;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Reflection;
using OffSync.Mapping.Mappert.Tests.Common;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests
{
    [TestFixture]
    public class MapperTest
    {
        static IMappingDelegateBuilder[] MappingDelegateBuilders = new IMappingDelegateBuilder[]
        {
            new ReflectionMappingDelegateBuilder(),
            new DynamicMethodMappingDelegateBuilder(),
        };

        [Test]
        [TestCaseSource(nameof(MappingDelegateBuilders))]
        public void Maps(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            var sut = new TestMapper(mappingDelegateBuilder);

            var source = new SourceModel()
            {
                Id = 1,
                Name = "2",
                Nested = new SourceNested()
                {
                    Key = 3,
                    Value = "4",
                },
                Values = "5,6",
                LookupValue = "7",
                ItemsEnumerable = new List<SourceNested>()
                {
                    new SourceNested()
                    {
                        Key = 8,
                        Value = "9",
                    },
                    new SourceNested()
                    {
                        Key = 10,
                        Value = "11",
                    },
                },
                ItemsArray = new SourceNested[]
                {
                    new SourceNested()
                    {
                        Key = 12,
                        Value = "13",
                    },
                    new SourceNested()
                    {
                        Key = 14,
                        Value = "15",
                    },
                },
                Numbers = new List<int>() { 16, 17 }.AsReadOnly(),
                Ignored = true,
                Shared = new SharedSub()
                {
                    Id = 16,
                    Label = "17",
                },
            };

            var target = sut.Map(source);

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

            Assert.That(
                target.Value1,
                Is.EqualTo("5"));

            Assert.That(
                target.Value2,
                Is.EqualTo("6"));

            Assert.That(
                target.LookupId,
                Is.EqualTo(7));

            Assert.That(
                target.ItemsArray,
                Has.Exactly(2).Items);

            Assert.That(
                target.ItemsArray[0].Key,
                Is.EqualTo(8));

            Assert.That(
                target.ItemsArray[0].Value,
                Is.EqualTo("9"));

            Assert.That(
                target.ItemsArray[1].Key,
                Is.EqualTo(10));

            Assert.That(
                target.ItemsArray[1].Value,
                Is.EqualTo("11"));

            Assert.That(
                target.ItemsCollection,
                Has.Exactly(2).Items);

            Assert.That(
                target.ItemsCollection[0].Key,
                Is.EqualTo(12));

            Assert.That(
                target.ItemsCollection[0].Value,
                Is.EqualTo("13"));

            Assert.That(
                target.ItemsCollection[1].Key,
                Is.EqualTo(14));

            Assert.That(
                target.ItemsCollection[1].Value,
                Is.EqualTo("15"));

            Assert.That(
                target.ItemsList,
                Has.Exactly(2).Items);

            Assert.That(
                target.ItemsList[0].Key,
                Is.EqualTo(12));

            Assert.That(
                target.ItemsList[0].Value,
                Is.EqualTo("13"));

            Assert.That(
                target.ItemsList[1].Key,
                Is.EqualTo(14));

            Assert.That(
                target.ItemsList[1].Value,
                Is.EqualTo("15"));

            CollectionAssert.AreEqual(
                source.Numbers,
                target.Numbers);

            Assert.That(
                target.Excluded,
                Is.False);

            Assert.That(
                target.Shared,
                Is.Not.Null);

            Assert.That(
                target.Shared.Id,
                Is.EqualTo(16));
        }

        [Test]
        [TestCaseSource(nameof(MappingDelegateBuilders))]
        public void MapShouldReturnNullOnNullSource(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            var sut = new TestMapper(mappingDelegateBuilder);

            var target = sut.Map(null);

            Assert.That(
                target,
                Is.Null);
        }
    }
}
