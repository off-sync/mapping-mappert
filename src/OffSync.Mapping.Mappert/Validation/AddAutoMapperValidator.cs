
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
                    $"auto-mapper not possible for multi-property mapping rule");
            }

            if (mappingRule.SourceProperties.Count == 0)
            {
                return Invalid(
                    $"auto-mapper not possible for target properties only mapping rule");
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
                    $"unable to create auto-mapper",
                    exception);
            }

            var builder = MappersUtil.CreateMapperDelegate(mapper);

            return SetBuilder(builder);
        }
    }
}
