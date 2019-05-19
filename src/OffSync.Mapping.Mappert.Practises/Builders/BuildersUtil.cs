using System;
using System.Linq;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Builders
{
    public static class BuildersUtil
    {
        public static BuilderTypes GetBuilderType(
            IMappingRule mappingRule)
        {
            #region Pre-conditions
            if (mappingRule == null)
            {
                throw new ArgumentNullException(nameof(mappingRule));
            }

            if (mappingRule.TargetProperties == null ||
                mappingRule.TargetProperties.Count == 0)
            {
                throw new ArgumentException(
                    $"{nameof(mappingRule.TargetProperties)} must contain at least one element",
                    nameof(mappingRule));
            }
            #endregion

            if (mappingRule.Builder == null)
            {
                return BuilderTypes.NoBuilder;
            }

            Type[] sourceTypes;
            Type[] targetTypes;

            switch (mappingRule.Type)
            {
                case MappingRuleTypes.MapToArray:
                case MappingRuleTypes.MapToCollection:
                    sourceTypes = new Type[] { mappingRule.SourceItemsType };
                    targetTypes = new Type[] { mappingRule.TargetItemsType };

                    break;

                // MappingRuleTypes.MapToValue:
                default:
                    sourceTypes = mappingRule.SourceProperties.Select(pi => pi.PropertyType).ToArray();
                    targetTypes = mappingRule.TargetProperties.Select(pi => pi.PropertyType).ToArray();

                    break;
            }

            var paramTypes = mappingRule.Builder.Method.GetParameters();

            if (paramTypes.Length != sourceTypes.Length)
            {
                throw new ArgumentException(
                    $"invalid number of builder parameters '{paramTypes.Length}', expected '{sourceTypes.Length}'");
            }

            for (int i = 0; i < sourceTypes.Length; i++)
            {
                if (!paramTypes[i].ParameterType.IsAssignableFrom(sourceTypes[i]))
                {
                    throw new ArgumentException(
                        $"invalid builder parameter at index '{i}': '{paramTypes[i].ParameterType.FullName}' must be assignable from '{sourceTypes[i].FullName}'");
                }
            }

            var returnType = mappingRule
                .Builder
                .Method
                .ReturnType;

            if (targetTypes.Length == 1)
            {
                if (!targetTypes[0].IsAssignableFrom(returnType))
                {
                    throw new ArgumentException(
                        $"invalid builder return type: '{targetTypes[0].FullName}' must be assignable from '{returnType.FullName}'");
                }

                return BuilderTypes.SingleValue;
            }

            // check if return type implements matching value tuple
            var valueTupleType = Type
                .GetType($"System.ValueTuple`{targetTypes.Length}")
                .MakeGenericType(targetTypes);

            if (returnType == valueTupleType)
            {
                return BuilderTypes.ValueTuple;
            }

            // check if return type implements object[]
            if (returnType == typeof(object[]))
            {
                return BuilderTypes.ObjectArray;
            }

            throw new ArgumentException(
                $"invalid builder return type '{returnType.FullName}', must implement matching ValueTuple or object[]");
        }
    }
}
