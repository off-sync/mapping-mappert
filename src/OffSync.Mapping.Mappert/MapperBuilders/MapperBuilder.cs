using System;
using System.Collections.Generic;
using System.Threading;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial class MapperBuilder<TSource, TTarget>
    {
        /// <summary>
        /// Stores the mapping rules that are added by calling AddMappingRule.
        /// </summary>
        private ICollection<MappingRule> _mappingRules = new List<MappingRule>();

        /// <summary>
        /// A read-write lock that ensures that the mapping rules are only
        /// checked once.
        /// </summary>
        private readonly ReaderWriterLockSlim _mappingRulesLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Whether the mapping rules have already been checked.
        /// </summary>
        private bool _mappingRulesChecked = false;

        /// <summary>
        /// Uses the <see cref="_mappingRulesLock" /> to
        /// protect against concurrent access. Executes the <paramref name="whenChecked"/>
        /// when a read lock has been acquired and the rules have already been checked.
        /// Upgrades to a write lock and executes <paramref name="whenUnchecked"/> if 
        /// the rules were not checked yet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whenChecked"></param>
        /// <param name="whenUnchecked"></param>
        /// <returns></returns>
        private T WithMappingRulesCheckedLocked<T>(
            Func<T> whenChecked,
            Func<T> whenUnchecked)
        {
            _mappingRulesLock.EnterUpgradeableReadLock();

            try
            {
                if (_mappingRulesChecked)
                {
                    return whenChecked();
                }

                _mappingRulesLock.EnterWriteLock();

                try
                {
                    return whenUnchecked();
                }
                finally
                {
                    _mappingRulesLock.ExitWriteLock();
                }
            }
            finally
            {
                _mappingRulesLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Adds a mapping rule for this builder and returns it.
        /// </summary>
        /// <returns>A new mapping rule that can be configured.</returns>
        /// <exception cref="InvalidOperationException">When the rules are checked.</exception>
        protected MappingRule AddMappingRule()
        {
            return WithMappingRulesCheckedLocked(
                () =>
                {
                    throw new InvalidOperationException(
                        $"{nameof(AddMappingRule)} only allowed when rules are unchecked");
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
        /// and sets <see cref="_mappingRulesChecked"/> to true afterwards.
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<MappingRule> GetCheckedMappingRules()
        {
            return WithMappingRulesCheckedLocked(
                () => _mappingRules,
                () =>
                {
                    CheckMappingRules(_mappingRules);

                    _mappingRulesChecked = true;

                    return _mappingRules;
                });
        }

        /// <summary>
        /// Is called only once when <see cref="GetCheckedMappingRules"/> is called and the 
        /// rules have not been checked yet. The provided mapping rules must be checked and 
        /// a checked version must be returned.
        /// <para>
        /// The default implementation performs the following checks:
        /// <list type="bullet">
        /// <item><description>Ensures that there are no duplicate target mappings.</description></item>
        /// <item><description>Ensures that all target properties are mapped.</description></item>
        /// <item><description>Ensures that all source properties are mapped.</description></item>
        /// <item><description>Removes any mappings without source or target properties.</description></item>
        /// <item><description>Ensures that all remaining mapping rules without a builder map exactly one source property to one target property.</description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="mappingRules"></param>
        /// <returns></returns>
        protected virtual void CheckMappingRules(
            ICollection<MappingRule> mappingRules)
        {
            MappingRulesUtil.EnsureNoDuplicateTargetMappings(mappingRules);

            MappingRulesUtil.EnsureAllTargetPropertiesMapped<TSource, TTarget>(mappingRules);

            MappingRulesUtil.EnsureAllSourcePropertiesMapped<TSource>(mappingRules);

            MappingRulesUtil.RemoveIgnoringMappingRules(mappingRules);

            MappingRulesUtil.EnsureValidBuilders(mappingRules);
        }
    }
}
