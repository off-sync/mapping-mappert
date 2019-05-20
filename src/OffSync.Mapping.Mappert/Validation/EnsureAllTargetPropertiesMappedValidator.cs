﻿using System.Collections.Generic;
using System.Linq;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class EnsureAllTargetPropertiesMappedValidator :
        AbstractMappingRuleSetValidator<MappingRule>
    {
        public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
            IEnumerable<MappingRule> mappingRules)
        {
            // create lookup of mapped target property names
            var mappedTargetPropertyNames = new HashSet<string>(mappingRules
                .SelectMany(mi => mi.TargetProperties)
                .Select(mi => mi.Name));

            // get unmapped target properties
            var targetProperties = MappingRulesUtil
                .GetMappableTargetProperties<TTarget>()
                .Where(pi => !mappedTargetPropertyNames.Contains(pi.Name));

            if (targetProperties.Any())
            {
                var names = string.Join(
                    "', '",
                    targetProperties
                        .Select(pi => pi.Name));

                return Invalid(
                    $"no mapping found for target properties: '{names}'");
            }

            return Valid();
        }
    }
}