using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;
using OffSync.Mapping.Mappert.Validation;
using OffSync.Mapping.Mappert.Validation.Exceptions;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    internal static class MapperBuilder
    {
        internal static readonly IEnumerable<IMappingValidator> DefaultValidators = new List<IMappingValidator>()
        {
            new NoDuplicateTargetMappingsValidator(),
            new AddAutoMappingValidator(),
            new EnsureAllTargetPropertiesMappedValidator(),
            new EnsureAllSourcePropertiesMappedValidator(),
            new RemoveIgnoringMappingRulesValidator(),
            new EnsureValidItemsMappingValidator(),
            new AddAutoMapperValidator(),
            new EnsureValidBuilderValidator(),
        };
    }

    public partial class MapperBuilder<TSource, TTarget>
    {
        private readonly ICollection<IMappingValidator> _validators = new List<IMappingValidator>();

        /// <summary>
        /// A read-write lock that ensures that the mapping rules are only validated once.
        /// </summary>
        private readonly ReaderWriterLockSlim _mappingRulesValidatedLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Whether the mapping rules have already been validated.
        /// </summary>
        private bool _mappingRulesValidated = false;

        /// <summary>
        /// Uses the <see cref="_mappingRulesValidatedLock" /> to
        /// protect against concurrent access. Executes the <paramref name="whenValidated"/>
        /// when a read lock has been acquired and the rules have already been checked.
        /// Upgrades to a write lock and executes <paramref name="whenNotValidated"/> if 
        /// the rules were not checked yet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whenValidated"></param>
        /// <param name="whenNotValidated"></param>
        /// <returns></returns>
        private T WithMappingRulesValidatedLocked<T>(
            Func<T> whenValidated,
            Func<T> whenNotValidated)
        {
            _mappingRulesValidatedLock.EnterUpgradeableReadLock();

            try
            {
                if (_mappingRulesValidated)
                {
                    return whenValidated();
                }

                _mappingRulesValidatedLock.EnterWriteLock();

                try
                {
                    return whenNotValidated();
                }
                finally
                {
                    _mappingRulesValidatedLock.ExitWriteLock();
                }
            }
            finally
            {
                _mappingRulesValidatedLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Is called only once when <see cref="GetValidatedMappingDelegate"/> is called and the 
        /// rules have not been validated yet. Calls <see cref="GetValidators"/> to get the list
        /// of validators to use and processes these in order.
        /// </summary>
        protected void ValidateMappingRules()
        {
            foreach (var validator in GetValidators())
            {
                if (validator is IMappingRuleValidator ruleValidator)
                {
                    ValidateMappingRules(ruleValidator);
                }

                if (validator is IMappingRuleSetValidator<MappingRule> ruleSetValidator)
                {
                    ValidateMappingRules(ruleSetValidator);
                }
            }
        }

        protected virtual IEnumerable<IMappingValidator> GetValidators()
        {
            if (_validators.Any())
            {
                return _validators;
            }

            return MapperBuilder.DefaultValidators;
        }

        private void ValidateMappingRules(
            IMappingRuleValidator ruleValidator)
        {
            foreach (var mappingRule in _mappingRules)
            {
                var result = ruleValidator.Validate<TSource, TTarget>(mappingRule);

                switch (result.Result)
                {
                    case MappingRuleValidationResults.Invalid:
                        throw new MappingRuleValidationException(
                            mappingRule,
                            result.Message,
                            result.Exception);

                    case MappingRuleValidationResults.SetBuilder:
                        mappingRule.WithBuilder(result.Builder);

                        break;
                }
            }
        }

        private void ValidateMappingRules(
            IMappingRuleSetValidator<MappingRule> ruleSetValidator)
        {
            var result = ruleSetValidator.Validate<TSource, TTarget>(_mappingRules);

            switch (result.Result)
            {
                case MappingRuleSetValidationResults.Invalid:
                    throw new MappingRuleSetValidationException(
                        result.Message,
                        result.Exception);

                case MappingRuleSetValidationResults.UpdateRules:
                    if (result.RulesToAdd.Any())
                    {
                        foreach (var rule in result.RulesToAdd.ToArray())
                        {
                            _mappingRules.Add(rule);
                        }
                    }

                    if (result.RulesToRemove.Any())
                    {
                        foreach (var rule in result.RulesToRemove.ToArray())
                        {
                            _mappingRules.Remove(rule);
                        }
                    }

                    break;
            }
        }
    }
}
