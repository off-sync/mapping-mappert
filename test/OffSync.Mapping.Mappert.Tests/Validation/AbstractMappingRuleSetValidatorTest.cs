using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class AbstractMappingRuleSetValidatorTest
    {
        public class TestMappingRuleSetValidator :
            AbstractMappingRuleSetValidator<MappingRule>
        {
            [ExcludeFromCodeCoverage]
            public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
                IEnumerable<MappingRule> mappingRules)
                => throw new System.NotImplementedException();

            public void InvokeInvalid(
                string message)
                => Invalid(message);

            public void InvokeUpdateRules(
                IEnumerable<MappingRule> rulesToAdd = null,
                IEnumerable<MappingRule> rulesToRemove = null)
                => UpdateRules(
                    rulesToAdd,
                    rulesToRemove);
        }

        private TestMappingRuleSetValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TestMappingRuleSetValidator();
        }

        [Test]
        public void InvalidShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.InvokeInvalid(null),
                Throws.ArgumentException);
        }

        [Test]
        public void UpdateRulesShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.InvokeUpdateRules(),
                Throws.ArgumentException);

            Assert.That(
                () => _sut.InvokeUpdateRules(
                    Enumerable.Empty<MappingRule>(),
                    Enumerable.Empty<MappingRule>()),
                Throws.ArgumentException);
        }
    }
}
