/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public interface IMappingRuleValidator :
        IMappingValidator
    {
        MappingRuleValidationResult Validate<TSource, TTarget>(
            IMappingRule mappingRule);
    }
}
