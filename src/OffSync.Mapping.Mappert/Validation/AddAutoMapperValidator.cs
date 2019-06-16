
using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class AddAutoMapperValidator :
        AbstractMappingRuleValidator
    {
        public override MappingRuleValidationResult Validate<TSource, TTarget>(
            IMappingRule mappingRule)
        {
            if (mappingRule.Builder != null ||
                mappingRule.TargetProperties.Count == 0)
            {
                // builder already present, or no target properties
                return Valid();
            }

            if (mappingRule.SourceProperties.Count > 1 ||
                mappingRule.TargetProperties.Count > 1)
            {
                return Invalid(
                    Messages.AutoMapperNotPossibleForMultiPropertyMappingRule);
            }

            if (mappingRule.SourceProperties.Count == 0)
            {
                return Invalid(
                    Messages.AutoMapperNotPossibleForTargetPropertiesOnlyMappingRule);
            }

            var sourceType = mappingRule.SourceItemsType ?? mappingRule.SourceProperties[0].PropertyType;

            var targetType = mappingRule.TargetItemsType ?? mappingRule.TargetProperties[0].PropertyType;

            if (targetType.IsAssignableFrom(sourceType))
            {
                // no builder required
                return Valid();
            }

            if (!MappersUtil.TryCreateAutoMapper(
                sourceType,
                targetType,
                out var mapper,
                out var exception))
            {
                return Invalid(
                    Messages.UnableToCreateAutoMapper,
                    exception);
            }

            var builder = MappersUtil.CreateMapperDelegate(mapper);

            return SetBuilder(builder);
        }
    }
}
