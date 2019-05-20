using NUnit.Framework;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Tests.Common;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Tests.Validation;
using OffSync.Mapping.Mappert.Validation.Exceptions;

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

        [Test]
        public void WithValidator()
        {
            var mapper = new TestMapper(b =>
            {
                b.WithValidator(
                    new TestMappingRuleSetValidator(
                        new MappingRuleSetValidationResult<MappingRule>()
                        {
                            Result = MappingRuleSetValidationResults.Valid,
                        }));
            });

            mapper.Map(null);
        }

        [Test]
        public void ValidateMappingRulesThrowsExceptions()
        {
            var mapper = new TestMapper(b =>
            {
                b.WithValidator(
                    new TestMappingRuleValidator(
                        new MappingRuleValidationResult()
                        {
                            Result = MappingRuleValidationResults.Invalid,
                            Message = "validation error",
                        }));

                // add at least one rule to trigger mapping rule validator
                b.Map(s => s.Id)
                    .To(t => t.Id);
            });

            Assert.That(
                () => mapper.Map(null),
                Throws.TypeOf<MappingRuleValidationException>());

            mapper = new TestMapper(b =>
            {
                b.WithValidator(
                    new TestMappingRuleSetValidator(
                        new MappingRuleSetValidationResult<MappingRule>()
                        {
                            Result = MappingRuleSetValidationResults.Invalid,
                            Message = "validation error",
                        }));
            });

            Assert.That(
                () => mapper.Map(null),
                Throws.TypeOf<MappingRuleSetValidationException>());
        }
    }
}
