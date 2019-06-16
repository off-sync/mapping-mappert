/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public interface IMappingRuleSetValidator<TMappingRule> :
        IMappingValidator
        where TMappingRule : IMappingRule
    {
        MappingRuleSetValidationResult<TMappingRule> Validate<TSource, TTarget>(
            IEnumerable<TMappingRule> mappingRules);
    }
}
