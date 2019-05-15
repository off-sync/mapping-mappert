using System;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public abstract class AbstractMappingRuleBuilder
    {
        protected readonly MappingRule _mappingRule;

        protected AbstractMappingRuleBuilder(
            MappingRule mappingRule)
        {
            #region Pre-conditions
            if (mappingRule == null)
            {
                throw new ArgumentNullException(nameof(mappingRule));
            }
            #endregion

            _mappingRule = mappingRule;
        }
    }
}
