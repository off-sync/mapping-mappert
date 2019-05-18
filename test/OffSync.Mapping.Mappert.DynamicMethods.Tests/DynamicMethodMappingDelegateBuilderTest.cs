using System;
using System.Reflection;

using NUnit.Framework;

namespace OffSync.Mapping.Mappert.DynamicMethods.Tests
{
    [TestFixture]
    public class DynamicMethodMappingDelegateBuilderTest
    {
        private DynamicMethodMappingDelegateBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DynamicMethodMappingDelegateBuilder();
        }

        [Test]
        public void CreateMappingDelegateShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.CreateMappingDelegate(
                    typeof(Source),
                    new PropertyInfo[] { typeof(Source).GetProperty(nameof(Source.Id)) },
                    typeof(Target),
                    new PropertyInfo[]
                    {
                        typeof(Target).GetProperty(nameof(Target.Id)),
                        typeof(Target).GetProperty(nameof(Target.Value1)),
                    },
                    null),
                Throws.ArgumentException);

            Assert.That(
                () => _sut.CreateMappingDelegate(
                    typeof(Source),
                    new PropertyInfo[] {
                        typeof(Source).GetProperty(nameof(Source.Id)),
                        typeof(Source).GetProperty(nameof(Source.Values)),
                    },
                    typeof(Target),
                    new PropertyInfo[]
                    {
                        typeof(Target).GetProperty(nameof(Target.Id)),
                    },
                    null),
                Throws.ArgumentException);
        }

        [Test]
        public void CreateMappingDelegateForDirectAssignment()
        {
            var mappingDelegate = _sut.CreateMappingDelegate(
                typeof(Source),
                new PropertyInfo[] { typeof(Source).GetProperty(nameof(Source.Id)) },
                typeof(Target),
                new PropertyInfo[] { typeof(Target).GetProperty(nameof(Target.Id)) },
                null);

            var source = new Source()
            {
                Id = 1,
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target,
                null);

            Assert.That(
                target.Id,
                Is.EqualTo(1));
        }

        [Test]
        public void CreateMappingDelegateForSingleValueAssignment()
        {
            Func<int, int> builder = i => i * 2;

            var mappingDelegate = _sut.CreateMappingDelegate(
                typeof(Source),
                new PropertyInfo[] { typeof(Source).GetProperty(nameof(Source.Id)) },
                typeof(Target),
                new PropertyInfo[] { typeof(Target).GetProperty(nameof(Target.Id)) },
                builder);

            var source = new Source()
            {
                Id = 1,
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target,
                builder);

            Assert.That(
                target.Id,
                Is.EqualTo(2));
        }

        [Test]
        public void CreateMappingDelegateForValueTupleAssignment()
        {
            Func<string, (int, string)> builder = TupleSplitter;

            var mappingDelegate = _sut.CreateMappingDelegate(
                typeof(Source),
                new PropertyInfo[] { typeof(Source).GetProperty(nameof(Source.Values)) },
                typeof(Target),
                new PropertyInfo[]
                {
                    typeof(Target).GetProperty(nameof(Target.Id)),
                    typeof(Target).GetProperty(nameof(Target.Value1)),
                },
                builder);

            var source = new Source()
            {
                Values = "1,2",
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target,
                builder);

            Assert.That(
                target.Id,
                Is.EqualTo(1));

            Assert.That(
                target.Value1,
                Is.EqualTo("2"));
        }

        private (int, string) TupleSplitter(
            string values)
        {
            var splitted = values.Split(',');

            return (int.Parse(splitted[0]), splitted[1]);
        }

        [Test]
        public void CreateMappingDelegateForObjectArrayAssignment()
        {
            Func<string, object[]> builder = ArraySplitter;

            var mappingDelegate = _sut.CreateMappingDelegate(
                typeof(Source),
                new PropertyInfo[] { typeof(Source).GetProperty(nameof(Source.Values)) },
                typeof(Target),
                new PropertyInfo[]
                {
                    typeof(Target).GetProperty(nameof(Target.Id)),
                    typeof(Target).GetProperty(nameof(Target.Value1)),
                },
                builder);

            var source = new Source()
            {
                Values = "1,2",
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target,
                builder);

            Assert.That(
                target.Id,
                Is.EqualTo(1));

            Assert.That(
                target.Value1,
                Is.EqualTo("2"));
        }

        private object[] ArraySplitter(
            string values)
        {
            var splitted = values.Split(',');

            return
                new object[]
                {
                    int.Parse(splitted[0]),
                    splitted[1],
                };
        }
    }
}
