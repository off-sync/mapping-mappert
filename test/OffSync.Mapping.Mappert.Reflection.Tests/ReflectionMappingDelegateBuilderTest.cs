using System;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Reflection.Tests
{
    [TestFixture]
    public class ReflectionMappingDelegateBuilderTest
    {
        private ReflectionMappingDelegateBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ReflectionMappingDelegateBuilder();
        }

        [Test]
        public void CreateMappingDelegateShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.CreateMappingDelegate<Source, Target>(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void CreateMappingDelegateForDirectAssignment()
        {
            var mappingRules = new IMappingRule[]
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Id)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Id))),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<Source, Target>(mappingRules);

            var source = new Source()
            {
                Id = 1,
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target);

            Assert.That(
                target.Id,
                Is.EqualTo(1));
        }

        [Test]
        public void CreateMappingDelegateForSingleValueAssignment()
        {
            Func<int, int> builder = i => i * 2;

            var mappingRules = new IMappingRule[]
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Id)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Id)))
                    .WithBuilder(builder),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<Source, Target>(mappingRules);

            var source = new Source()
            {
                Id = 1,
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target);

            Assert.That(
                target.Id,
                Is.EqualTo(2));
        }

        [Test]
        public void CreateMappingDelegateForValueTupleAssignment()
        {
            Func<string, (int, string)> builder = TupleSplitter;

            var mappingRules = new IMappingRule[]
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Values)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Id)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Value1)))
                    .WithBuilder(builder),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<Source, Target>(mappingRules);

            var source = new Source()
            {
                Values = "1,2",
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target);

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

            var mappingRules = new IMappingRule[]
            {
                new MappingRule()
                    .WithSource(typeof(Source).GetProperty(nameof(Source.Values)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Id)))
                    .WithTarget(typeof(Target).GetProperty(nameof(Target.Value1)))
                    .WithBuilder(builder),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<Source, Target>(mappingRules);

            var source = new Source()
            {
                Values = "1,2",
            };

            var target = new Target();

            mappingDelegate.DynamicInvoke(
                source,
                target);

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
