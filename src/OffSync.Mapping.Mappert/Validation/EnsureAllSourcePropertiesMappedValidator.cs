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
    public class EnsureAllSourcePropertiesMappedValidator :
        AbstractMappingRuleSetValidator<MappingRule>
    {
        public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
            IEnumerable<MappingRule> mappingRules)
        {
            // create lookup of mapped source property names
            var mappedSourcePropertyNames = new HashSet<string>(mappingRules
                .SelectMany(mi => mi.SourceProperties)
                .Select(mi => mi.Name));

            // get unmapped source properties
            var sourceProperties = MappingRulesUtil
                .GetMappableSourceProperties<TSource>()
                .Where(pi => !mappedSourcePropertyNames.Contains(pi.Name));

            if (sourceProperties.Any())
            {
                var names = string.Join(
                    "', '",
                    sourceProperties
                        .Select(pi => pi.Name));

                return Invalid(
                    string.Format(
                        Messages.NoMappingFoundForSourceProperties,
                        names));
            }

            return Valid();
        }
    }
}
