﻿using System.Collections.Generic;
using System.Linq;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class NoDuplicateTargetMappingsValidator :
        AbstractMappingRuleSetValidator<MappingRule>
    {
        public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
            IEnumerable<MappingRule> mappingRules)
        {
            var targetDuplicates = mappingRules
                .SelectMany(mi => mi.TargetProperties)
                .GroupBy(pi => pi.Name)
                .Where(g => g.Count() > 1);

            if (targetDuplicates.Any())
            {
                var names = string.Join(
                    "', '",
                    targetDuplicates.Select(g => g.Key));

                return Invalid(
                    $"duplicate mapping found for target properties: '{names}'");
            }

            return Valid();
        }
    }
}