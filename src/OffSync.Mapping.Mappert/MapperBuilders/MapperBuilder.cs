using System;
using System.Collections.Generic;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial class MapperBuilder<TSource, TTarget>
    {
        public MapperBuilder()
        {
        }

        public MapperBuilder(
            Action<IMapperBuilder<TSource, TTarget>> configure)
        {
            #region Pre-conditions
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            #endregion

            configure(this);
        }

        /// <summary>
        /// Stores the mapping rules that are added by calling AddMappingRule.
        /// </summary>
        private IList<MappingRule> _mappingRules = new List<MappingRule>();

        private MappingDelegate<TSource, TTarget> _mappingDelegate = null;

        /// <summary>
        /// Adds a mapping rule for this builder and returns it.
        /// </summary>
        /// <returns>A new mapping rule that can be configured.</returns>
        /// <exception cref="InvalidOperationException">When the rules are checked.</exception>
        protected MappingRule AddMappingRule()
        {
            return WithMappingRulesValidatedLocked(
                () =>
                {
                    throw new InvalidOperationException(
                        $"{nameof(AddMappingRule)} only allowed before rules have been checked");
                },
                () =>
                {
                    var mappingRule = new MappingRule();

                    _mappingRules.Add(mappingRule);

                    return mappingRule;
                });
        }

        /// <summary>
        /// Returns the checked mapping rules. Checks the rules using <see cref="CheckMappingRules(IEnumerable{MappingRule})"/>
        /// and sets <see cref="_mappingRulesValidated"/> to true afterwards.
        /// </summary>
        /// <returns></returns>
        protected MappingDelegate<TSource, TTarget> GetValidatedMappingDelegate()
        {
            return WithMappingRulesValidatedLocked(
                () => _mappingDelegate,
                () =>
                {
                    ValidateMappingRules();

                    _mappingDelegate = CreateMappingDelegate(_mappingRules);

                    _mappingRulesValidated = true;

                    return _mappingDelegate;
                });
        }

        protected virtual MappingDelegate<TSource, TTarget> CreateMappingDelegate(
            IEnumerable<IMappingRule> mappingRules)
        {
            if (_mappingDelegateBuilder == null)
            {
                _mappingDelegateBuilder = ConfigurationUtil.GetRegisteredMappingDelegateBuilder();
            }

            return _mappingDelegateBuilder.CreateMappingDelegate<TSource, TTarget>(mappingRules);
        }
    }
}
