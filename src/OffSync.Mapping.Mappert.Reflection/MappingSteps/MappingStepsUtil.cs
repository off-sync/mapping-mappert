using System.Linq;
using System.Reflection;

using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Reflection.MappingSteps
{
    public static class MappingStepsUtil
    {
        public static MappingStep Create(
            IMappingRule mappingRule)
        {
            var builderType = BuildersUtil.GetBuilderType(mappingRule);

            var valueTupleFields = new FieldInfo[0];

            if (builderType == BuilderTypes.ValueTuple)
            {
                // pre-fetch value tuple item field infos
                var returnType = mappingRule
                    .Builder
                    .Method
                    .ReturnType;

                valueTupleFields = Enumerable
                    .Range(1, mappingRule.TargetProperties.Count)
                    .Select(i => returnType.GetField($"Item{i}"))
                    .ToArray();
            }

            MethodInfo builderInvoke = null;

            if (mappingRule.Builder != null)
            {
                var paramTypes = mappingRule
                    .Builder
                    .Method
                    .GetParameters()
                    .Select(pi => pi.ParameterType)
                    .ToArray();

                builderInvoke = mappingRule
                    .Builder
                    .GetType()
                    .GetMethod(
                        "Invoke",
                        paramTypes);
            }

            return new MappingStep()
            {
                SourceProperties = mappingRule.SourceProperties.ToArray(),
                TargetProperties = mappingRule.TargetProperties.ToArray(),
                TargetItemType = mappingRule.TargetItemsType,
                MappingRuleType = mappingRule.Type,
                Builder = mappingRule.Builder,
                BuilderInvoke = builderInvoke,
                BuilderType = builderType,
                ValueTupleFields = valueTupleFields,
            };
        }
    }
}
