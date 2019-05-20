using System.Linq;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.Validation
{
    public class EnsureValidItemsMappingValidator :
        AbstractMappingRuleValidator
    {
        public override MappingRuleValidationResult Validate<TSource, TTarget>(
            IMappingRule mappingRule)
        {
            if (mappingRule.Type != MappingRuleTypes.MapToArray &&
                mappingRule.Type != MappingRuleTypes.MapToCollection)
            {
                // not an items mapping
                return Valid();
            }

            if (mappingRule.SourceProperties.Count != 1)
            {
                var sourceNames = string.Join(
                    "', '",
                    mappingRule
                        .SourceProperties
                        .Select(pi => pi.Name));

                return Invalid(
                    $"exactly one source property must be mapped for an items mapping, found: '{sourceNames}'");
            }

            if (mappingRule.TargetProperties.Count != 1)
            {
                var targetNames = string.Join(
                    "', '",
                    mappingRule
                        .TargetProperties
                        .Select(pi => pi.Name));

                return Invalid(
                    $"exactly one target property must be mapped for an items mapping, found: '{targetNames}'");
            }

            if (!ItemsUtil.TryGetSourceItemsType(
                mappingRule.SourceProperties[0].PropertyType,
                out var sourceItemsType))
            {
                return Invalid(
                    $"unable to determine source items type for property: '{mappingRule.SourceProperties[0].Name}'");
            }

            if (!ItemsUtil.TryGetTargetItemsType(
                mappingRule.TargetProperties[0].PropertyType,
                out var targetItemsType))
            {
                return Invalid(
                    $"unable to determine target items type for property: '{mappingRule.TargetProperties[0].Name}'");
            }

            return Valid();
        }
    }
}
