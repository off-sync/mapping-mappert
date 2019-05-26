using System;
using System.Collections.Generic;
using System.Linq;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public abstract class AbstractMappingRuleSetValidator<TMappingRule> :
        IMappingRuleSetValidator<TMappingRule>
        where TMappingRule : IMappingRule
    {
        public abstract MappingRuleSetValidationResult<TMappingRule> Validate<TSource, TTarget>(IEnumerable<TMappingRule> mappingRules);

        #region Helpers
        protected MappingRuleSetValidationResult<TMappingRule> Valid()
        {
            return new MappingRuleSetValidationResult<TMappingRule>()
            {
                Result = MappingRuleSetValidationResults.Valid,
            };
        }

        protected MappingRuleSetValidationResult<TMappingRule> Invalid(
            string message,
            Exception exception = null)
        {
            #region Pre-conditions
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(
                    "message must be provided",
                    nameof(message));
            }
            #endregion

            return new MappingRuleSetValidationResult<TMappingRule>()
            {
                Result = MappingRuleSetValidationResults.Invalid,
                Message = message,
                Exception = exception,
            };
        }

        protected MappingRuleSetValidationResult<TMappingRule> UpdateRules(
            IEnumerable<TMappingRule> rulesToAdd = null,
            IEnumerable<TMappingRule> rulesToRemove = null)
        {
            #region Pre-conditions
            if ((rulesToAdd == null ||
                !rulesToAdd.Any()) &&
                (rulesToRemove == null ||
                !rulesToRemove.Any()))
            {
                throw new ArgumentException(
                    $"either {nameof(rulesToAdd)} or {nameof(rulesToRemove)} must be non-null and contain elements");
            }
            #endregion

            return new MappingRuleSetValidationResult<TMappingRule>()
            {
                Result = MappingRuleSetValidationResults.UpdateRules,
                RulesToAdd = rulesToAdd ?? Enumerable.Empty<TMappingRule>(),
                RulesToRemove = rulesToRemove ?? Enumerable.Empty<TMappingRule>(),
            };
        }
        #endregion
    }
}
