using System;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Tests.Models;

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
        public void IsRegistered()
        {
            var builder = ConfigurationUtil.GetRegisteredMappingDelegateBuilder();

            Assert.That(
                builder,
                Is.TypeOf<ReflectionMappingDelegateBuilder>());
        }

        [Test]
        public void CreateMappingDelegateShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.CreateMappingDelegate<SourceModel, TargetModel>(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void CreateMappingDelegateForDirectAssignment()
        {
            var mappingRules = new IMappingRule[]
            {
                new MappingRule()
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id))),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<SourceModel, TargetModel>(mappingRules);

            var source = new SourceModel()
            {
                Id = 1,
            };

            var target = new TargetModel();

            mappingDelegate(
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
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                    .WithBuilder(builder),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<SourceModel, TargetModel>(mappingRules);

            var source = new SourceModel()
            {
                Id = 1,
            };

            var target = new TargetModel();

            mappingDelegate(
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
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                    .WithBuilder(builder),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<SourceModel, TargetModel>(mappingRules);

            var source = new SourceModel()
            {
                Values = "1,2",
            };

            var target = new TargetModel();

            mappingDelegate(
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
                    .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)))
                    .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                    .WithBuilder(builder),
            };

            var mappingDelegate = _sut.CreateMappingDelegate<SourceModel, TargetModel>(mappingRules);

            var source = new SourceModel()
            {
                Values = "1,2",
            };

            var target = new TargetModel();

            mappingDelegate(
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
