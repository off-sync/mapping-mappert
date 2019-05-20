using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public interface IMappingRuleValidator :
        IMappingValidator
    {
        MappingRuleValidationResult Validate<TSource, TTarget>(
            IMappingRule mappingRule);
    }
}
