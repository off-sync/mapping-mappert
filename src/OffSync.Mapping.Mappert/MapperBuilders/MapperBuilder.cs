using System;
using System.Collections.Generic;
using System.Threading;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Reflection;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial class MapperBuilder<TSource, TTarget>
    {
        public MapperBuilder()
        {
        }

        public MapperBuilder(
            Action<IMapperBuilder<TSource, TTarget>> withMappingRules)
        {
            #region Pre-conditions
            if (withMappingRules == null)
            {
                throw new ArgumentNullException(nameof(withMappingRules));
            }
            #endregion

            withMappingRules(this);
        }

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

        private MappingDelegate<TSource, TTarget> _mappingDelegate = null;

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
        /// and sets <see cref="_mappingRulesChecked"/> to true afterwards.
        /// </summary>
        /// <returns></returns>
        protected MappingDelegate<TSource, TTarget> GetCheckedMappingDelegate()
        {
            return WithMappingRulesCheckedLocked(
                () => _mappingDelegate,
                () =>
                {
                    var checkedMappingRules = CheckMappingRules(_mappingRules);

                    _mappingDelegate = CreateMappingDelegate(checkedMappingRules);

                    _mappingRulesChecked = true;

                    return _mappingDelegate;
                });
        }

        /// <summary>
        /// Is called only once when <see cref="GetCheckedMappingDelegate"/> is called and the 
        /// rules have not been checked yet. The provided mapping rules must be checked and can
        /// be changed to make the complete set valid.
        /// <para>
        /// The default implementation performs the following checks:
        /// <list type="bullet">
        /// <item><description>Ensures that there are no duplicate target mappings.</description></item>
        /// <item><description>Ensures that all target properties are mapped.</description></item>
        /// <item><description>Ensures that all source properties are mapped.</description></item>
        /// <item><description>Removes any mappings without target properties, or without source properties and no builder.</description></item>
        /// <item><description>Ensures that all remaining mapping rules have a valid builder where required.</description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="mappingRules"></param>
        /// <returns></returns>
        protected virtual IEnumerable<IMappingRule> CheckMappingRules(
            IEnumerable<IMappingRule> mappingRules)
        {
            MappingRulesUtil.EnsureNoDuplicateTargetMappings(mappingRules);

            mappingRules = MappingRulesUtil.WithAllTargetPropertiesMapped<TSource, TTarget>(mappingRules);

            MappingRulesUtil.EnsureAllSourcePropertiesMapped<TSource>(mappingRules);

            mappingRules = MappingRulesUtil.WithoutIgnoringMappingRules(mappingRules);

            mappingRules = MappingRulesUtil.WithAutoMappingBuilders(mappingRules);

            MappingRulesUtil.EnsureValidBuilders(mappingRules);

            MappingRulesUtil.EnsureValidItemsMappings(mappingRules);

            return mappingRules;
        }

        protected virtual MappingDelegate<TSource, TTarget> CreateMappingDelegate(
            IEnumerable<IMappingRule> mappingRules)
        {
            if (_mappingDelegateBuilder == null)
            {
                _mappingDelegateBuilder = new ReflectionMappingDelegateBuilder();
            }

            return _mappingDelegateBuilder.CreateMappingDelegate<TSource, TTarget>(mappingRules);
        }
    }
}
