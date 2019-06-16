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
                    string.Format(
                        Messages.ExactlyOneSourcePropertyMustBeMappedForItemsMapping,
                        sourceNames));
            }

            if (mappingRule.TargetProperties.Count != 1)
            {
                var targetNames = string.Join(
                    "', '",
                    mappingRule
                        .TargetProperties
                        .Select(pi => pi.Name));

                return Invalid(
                    string.Format(
                        Messages.ExactlyOneTargetPropertyMustBeMappedForItemsMappingFound,
                        targetNames));
            }

            if (!ItemsUtil.TryGetSourceItemsType(
                mappingRule.SourceProperties[0].PropertyType,
                out var sourceItemsType))
            {
                return Invalid(
                    string.Format(
                        Messages.UnableToDetermineSourceItemsType,
                        mappingRule.SourceProperties[0].Name));
            }

            if (!ItemsUtil.TryGetTargetItemsType(
                mappingRule.TargetProperties[0].PropertyType,
                out var targetItemsType))
            {
                return Invalid(
                    string.Format(
                        Messages.UnableToDetermineTargetItemsType,
                        mappingRule.TargetProperties[0].Name));
            }

            return Valid();
        }
    }
}
