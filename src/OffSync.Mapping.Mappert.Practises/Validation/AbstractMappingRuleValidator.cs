using System;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public abstract class AbstractMappingRuleValidator :
        IMappingRuleValidator
    {
        public abstract MappingRuleValidationResult Validate<TSource, TTarget>(IMappingRule mappingRule);

        #region Helpers
        protected MappingRuleValidationResult Valid()
        {
            return new MappingRuleValidationResult()
            {
                Result = MappingRuleValidationResults.Valid,
            };
        }

        protected MappingRuleValidationResult Invalid(
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

            return new MappingRuleValidationResult()
            {
                Result = MappingRuleValidationResults.Invalid,
                Message = message,
                Exception = exception,
            };
        }

        protected MappingRuleValidationResult SetBuilder(
            Delegate builder)
        {
            #region Pre-conditions
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            #endregion

            return new MappingRuleValidationResult()
            {
                Result = MappingRuleValidationResults.SetBuilder,
                Builder = builder,
            };
        }
        #endregion
    }
}
