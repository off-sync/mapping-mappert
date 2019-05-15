using System;
using System.Linq;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static partial class MappingRulesExtensions
    {
        public static void Apply<TSource, TTarget>(
            this MappingRule mappingRule,
            TSource source,
            TTarget target)
        {
            var targetPropertyCount = mappingRule.TargetProperties.Count;

            object value;

            if (mappingRule.Builder == null)
            {
                // get value from single source property
                value = mappingRule
                    .SourceProperties[0]
                    .GetValue(source);
            }
            else
            {
                // use builder to get value from source properties
                var froms = mappingRule
                    .SourceProperties
                    .Select(pi => pi.GetValue(source))
                    .ToArray();

                value = mappingRule
                    .Builder
                    .DynamicInvoke(froms);
            }

            if (targetPropertyCount == 1)
            {
                // set value of single target property
                mappingRule
                    .TargetProperties[0]
                    .SetValue(target, value);

                return;
            }

            // multi-valued assignment using ValueTuple
            if (value.GetType().IsGenericType &&
                value.GetType().GetGenericTypeDefinition().Name == $"ValueTuple`{targetPropertyCount}")
            {
                mappingRule.ApplyFromValueTuple(
                    target,
                    value);

                return;
            }

            // multi-valued assignment using Tuple
            if (value.GetType().IsGenericType &&
                value.GetType().GetGenericTypeDefinition().Name == $"Tuple`{targetPropertyCount}")
            {
                mappingRule.ApplyFromTuple(
                    target,
                    value);

                return;
            }

            // multi-valued assignment using object array
            if (value.GetType().IsArray &&
                ((object[])value).Length == targetPropertyCount)
            {
                mappingRule.ApplyFromArray(
                    target,
                    (object[])value);

                return;
            }

            throw new InvalidOperationException(
                $"invalid builder result: {value.GetType().FullName}");
        }

        public static void ApplyFromValueTuple<TTarget>(
            this MappingRule mappingRule,
            TTarget target,
            object value)
        {
            var type = value.GetType();

            for (int i = 0; i < mappingRule.TargetProperties.Count; i++)
            {
                var item = type
                    .GetField($"Item{i + 1}")
                    .GetValue(value);

                mappingRule
                    .TargetProperties[i]
                    .SetValue(
                        target,
                        item);
            }
        }

        public static void ApplyFromTuple<TTarget>(
            this MappingRule mappingRule,
            TTarget target,
            object value)
        {
            var type = value.GetType();

            for (int i = 0; i < mappingRule.TargetProperties.Count; i++)
            {
                var item = type
                    .GetProperty($"Item{i + 1}")
                    .GetValue(value);

                mappingRule
                    .TargetProperties[i]
                    .SetValue(
                        target,
                        item);
            }
        }

        public static void ApplyFromArray<TTarget>(
            this MappingRule mappingRule,
            TTarget target,
            object[] value)
        {
            for (int i = 0; i < mappingRule.TargetProperties.Count; i++)
            {
                mappingRule
                    .TargetProperties[i]
                    .SetValue(
                        target,
                        value[i]);
            }
        }
    }
}
