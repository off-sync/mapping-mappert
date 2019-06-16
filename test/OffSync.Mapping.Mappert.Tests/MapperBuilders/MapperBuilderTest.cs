/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Moq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.MapperBuilders
{
    [TestFixture]
    public partial class MapperBuilderTest
    {
        public class TestMapperBuilder :
            MapperBuilder<SourceModel, TargetModel>
        {
            public TestMapperBuilder(
                IMappingRuleValidator ruleValidator,
                IMappingRuleSetValidator<MappingRule> ruleSetValidator)
            {
                WithValidator(ruleValidator);

                WithValidator(ruleSetValidator);
            }
        }

        [Test]
        public void ConstructorShouldCheckPreConditions()
        {
            Assert.That(
                () => new MapperBuilder<SourceModel, TargetModel>(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void ShouldAcceptValidators()
        {
            var ruleValidator = new Mock<IMappingRuleValidator>();

            var ruleSetValidator = new Mock<IMappingRuleSetValidator<MappingRule>>();

            new MapperBuilder<SourceModel, TargetModel>(
                b =>
                {
                    b.WithValidator(ruleValidator.Object);

                    b.WithValidator(ruleSetValidator.Object);
                });

            new TestMapperBuilder(
                ruleValidator.Object,
                ruleSetValidator.Object);
        }
    }
}
