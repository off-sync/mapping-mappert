/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public class MappingRuleSetValidationResult<TMappingRule>
        where TMappingRule : IMappingRule
    {
        public MappingRuleSetValidationResults Result { get; set; }

        #region Invalid results
        public string Message { get; set; }

        public Exception Exception { get; set; }
        #endregion

        #region AddOrRemoveRules results
        public IEnumerable<TMappingRule> RulesToAdd { get; set; }

        public IEnumerable<TMappingRule> RulesToRemove { get; set; }
        #endregion
    }
}