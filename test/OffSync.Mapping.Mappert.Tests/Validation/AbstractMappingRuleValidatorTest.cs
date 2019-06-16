/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Tests.Validation
{
    [TestFixture]
    public class AbstractMappingRuleValidatorTest
    {
        public class TestMappingRuleValidator :
            AbstractMappingRuleValidator
        {
            [ExcludeFromCodeCoverage]
            public override MappingRuleValidationResult Validate<TSource, TTarget>(
                IMappingRule mappingRule)
                => throw new System.NotImplementedException();

            public void InvokeInvalid(
                string message)
                => Invalid(message);

            public void InvokeSetBuilder(
                Delegate builder)
                => SetBuilder(builder);
        }

        private TestMappingRuleValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TestMappingRuleValidator();
        }

        [Test]
        public void InvalidShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.InvokeInvalid(null),
                Throws.ArgumentException);
        }

        [Test]
        public void SetBuilderShouldCheckPreConditions()
        {
            Assert.That(
                () => _sut.InvokeSetBuilder(null),
                Throws.ArgumentNullException);
        }
    }
}
