using System.Collections.Generic;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    public class TestMappingRuleSetValidator :
        AbstractMappingRuleSetValidator<MappingRule>
    {
        private readonly MappingRuleSetValidationResult<MappingRule> _result;

        public TestMappingRuleSetValidator(
            MappingRuleSetValidationResult<MappingRule> result)
        {
            _result = result;
        }

        public override MappingRuleSetValidationResult<MappingRule> Validate<TSource, TTarget>(
            IEnumerable<MappingRule> mappingRules)
        {
            return _result;
        }
    }
}
