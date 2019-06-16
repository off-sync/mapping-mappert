/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public abstract class AbstractMappingRuleBuilder
    {
        protected readonly MappingRule _mappingRule;

        protected AbstractMappingRuleBuilder(
            MappingRule mappingRule)
        {
            #region Pre-conditions
            if (mappingRule == null)
            {
                throw new ArgumentNullException(nameof(mappingRule));
            }
            #endregion

            _mappingRule = mappingRule;
        }
    }
}
