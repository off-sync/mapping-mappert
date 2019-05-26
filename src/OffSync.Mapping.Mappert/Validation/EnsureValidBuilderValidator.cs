using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class EnsureValidBuilderValidator :
        AbstractMappingRuleValidator
    {
        public override MappingRuleValidationResult Validate<TSource, TTarget>(
            IMappingRule mappingRule)
        {
            if (mappingRule.Builder == null)
            {
                if (mappingRule.SourceProperties.Count > 1 ||
                    mappingRule.TargetProperties.Count > 1)
                {
                    return Invalid(
                        "builder required for multi-property mapping rule");
                }

                if (mappingRule.SourceProperties.Count == 0)
                {
                    return Invalid(
                        "builder required for target-properties only mapping rule");
                }

                return Valid();
            }

            if (!BuildersUtil.TryGetBuilderType(
                mappingRule,
                out _,
                out var exception))
            {
                return Invalid(
                    $"invalid builder type: {mappingRule.Builder.GetType()}",
                    exception);
            }

            return Valid();
        }
    }
}
