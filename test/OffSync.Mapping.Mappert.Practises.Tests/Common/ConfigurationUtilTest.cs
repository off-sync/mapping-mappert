using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Practises.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Tests.Common
{
    [TestFixture]
    public class ConfigurationUtilTest
    {
        [ExcludeFromCodeCoverage]
        class TestMappingDelegateBuilder :
            IMappingDelegateBuilder
        {
            public MappingDelegate<TSource, TTarget> CreateMappingDelegate<TSource, TTarget>(
                IEnumerable<IMappingRule> mappingRules)
                => throw new System.NotImplementedException();
        }

        [Test]
        public void EnsureValidMappingDelegateBuilderType()
        {
            Assert.That(
                () => ConfigurationUtil.EnsureValidMappingDelegateBuilderType(null),
                Throws.ArgumentNullException);

            Assert.That(
                () => ConfigurationUtil.EnsureValidMappingDelegateBuilderType(typeof(string)),
                Throws.ArgumentException);

            ConfigurationUtil.EnsureValidMappingDelegateBuilderType(typeof(TestMappingDelegateBuilder));
        }

        [Test]
        public void GetRegisteredMappingDelegateBuilder()
        {
            Assert.That(
                () => ConfigurationUtil.GetRegisteredMappingDelegateBuilder(),
                Throws.InvalidOperationException);
        }
    }
}
