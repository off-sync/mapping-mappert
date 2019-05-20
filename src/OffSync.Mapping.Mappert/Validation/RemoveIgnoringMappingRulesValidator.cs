using System.Collections.Generic;
using System.Linq;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class RemoveIgnoringMappingRulesValidator :
        AbstractMappingRuleSetValidator<MappingRule>
    {
        public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
            IEnumerable<MappingRule> mappingRules)
        {
            var rulesToRemove = mappingRules
                .Where(r => IsIgnoringMappingRule(r));

            if (rulesToRemove.Any())
            {
                return AddOrRemoveRules(rulesToRemove: rulesToRemove);
            }

            return Valid();
        }

        private static bool IsIgnoringMappingRule(
            IMappingRule mappingRule)
        {
            if (!mappingRule.TargetProperties.Any())
            {
                // no target properties
                return true;
            }

            if (!mappingRule.SourceProperties.Any() &&
                mappingRule.Builder == null)
            {
                // no source properties, and no builder
                return true;
            }

            return false;
        }
    }
}
