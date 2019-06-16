/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class AddAutoMappingValidator :
        AbstractMappingRuleSetValidator<MappingRule>
    {
        public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
            IEnumerable<MappingRule> mappingRules)
        {
            var rulesToAdd = new List<MappingRule>();

            // create lookup of mapped target property names
            var mappedTargetPropertyNames = new HashSet<string>(mappingRules
                .SelectMany(mi => mi.TargetProperties)
                .Select(mi => mi.Name));

            // get all mappable target properties without a mapping
            var targetProperties = MappingRulesUtil
                .GetMappableTargetProperties<TTarget>()
                .Where(pi => !mappedTargetPropertyNames.Contains(pi.Name));

            // check if all target properties are mapped
            foreach (var targetProperty in targetProperties)
            {
                // try to add an auto-mapping
                if (!MappingRulesUtil.TryCreateAutoMapping<TSource>(
                    targetProperty,
                    out var mappingRule,
                    out var exception))
                {
                    return Invalid(
                        string.Format(
                            Messages.UnableToCreateAutoMappingForProperty,
                            targetProperty.Name),
                        exception);
                }

                // add auto-mapping rule
                rulesToAdd.Add(mappingRule);
            }

            if (rulesToAdd.Any())
            {
                return UpdateRules(rulesToAdd);
            }

            return Valid();
        }
    }
}
