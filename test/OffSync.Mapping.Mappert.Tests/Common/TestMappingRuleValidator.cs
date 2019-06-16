/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    public class TestMappingRuleValidator :
        AbstractMappingRuleValidator
    {
        private readonly MappingRuleValidationResult _result;

        public TestMappingRuleValidator(
            MappingRuleValidationResult result)
        {
            _result = result;
        }

        public override MappingRuleValidationResult Validate<TSource, TTarget>(
            IMappingRule mappingRule)
        {
            return _result;
        }
    }
}
