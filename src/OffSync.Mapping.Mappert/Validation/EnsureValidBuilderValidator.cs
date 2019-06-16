/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Common;
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
                        Messages.BuilderRequiredForMultiPropertyMappingRule);
                }

                if (mappingRule.SourceProperties.Count == 0)
                {
                    return Invalid(
                        Messages.BuilderRequiredForTargetPropertiesOnlyMappingRule);
                }

                return Valid();
            }

            if (!BuildersUtil.TryGetBuilderType(
                mappingRule,
                out _,
                out var exception))
            {
                return Invalid(
                    string.Format(
                        Messages.InvalidBuilderType,
                        mappingRule.Builder.GetType()),
                    exception);
            }

            return Valid();
        }
    }
}
