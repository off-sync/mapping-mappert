/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public class MappingRuleValidationResult
    {
        public MappingRuleValidationResults Result { get; set; }

        #region Invalid results
        public string Message { get; set; }

        public Exception Exception { get; set; }
        #endregion

        #region SetBuilder results
        public Delegate Builder { get; set; }
        #endregion
    }
}