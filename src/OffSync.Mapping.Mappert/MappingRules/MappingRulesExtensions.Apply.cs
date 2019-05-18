using System;
using System.Collections;
using System.Linq;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static partial class MappingRulesExtensions
    {
        public static void Apply<TSource, TTarget>(
            this MappingRule mappingRule,
            TSource source,
            TTarget target)
        {
            switch (mappingRule.MappingStrategy)
            {
                case MappingStrategies.MapToValue:
                    mappingRule.ApplyToValue(
                        source,
                        target);

                    return;

                case MappingStrategies.MapToArray:
                    mappingRule.ApplyToArray(
                        source,
                        target);

                    return;

                case MappingStrategies.MapToCollection:
                    mappingRule.ApplyToCollection(
                        source,
                        target);

                    return;
            }
        }

        private static void ApplyToValue<TSource, TTarget>(
            this MappingRule mappingRule,
            TSource source,
            TTarget target)
        {
            var value = mappingRule.GetValue(source);

            var targetPropertyCount = mappingRule.TargetProperties.Count;

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

        private static object GetValue<TSource>(
            this MappingRule mappingRule,
            TSource source)
        {
            if (mappingRule.Builder == null)
            {
                // get value from single source property
                return mappingRule
                    .SourceProperties[0]
                    .GetValue(source);
            }

            // use builder to get value from source properties
            var froms = mappingRule
                .SourceProperties
                .Select(pi => pi.GetValue(source))
                .ToArray();

            return mappingRule
                .Builder
                .DynamicInvoke(froms);
        }

        private static void ApplyToArray<TSource, TTarget>(
            this MappingRule mappingRule,
            TSource source,
            TTarget target)
        {
            // get source value
            var value = mappingRule
                .SourceProperties[0]
                .GetValue(source);

            // get length of new array
            var length = ItemsUtil.GetItemsCount(value);

            // create new array
            var array = ItemsUtil.CreateArray(
                mappingRule.TargetTypes[0],
                length);

            // loop source values and set to array
            var i = 0;

            foreach (var item in (IEnumerable)value)
            {
                var targetItem = mappingRule.GetTargetItemValue(item);

                array.SetValue(
                    targetItem,
                    i++);
            }

            // set target value
            mappingRule
                .TargetProperties[0]
                .SetValue(target, array);
        }

        private static void ApplyToCollection<TSource, TTarget>(
            this MappingRule mappingRule,
            TSource source,
            TTarget target)
        {
            // get source value
            var value = mappingRule
                .SourceProperties[0]
                .GetValue(source);

            // create new collection
            var collection = ItemsUtil.CreateCollection(
                mappingRule.TargetProperties[0].PropertyType,
                mappingRule.TargetTypes[0]);

            var addMethod = collection
                .GetType()
                .GetMethod(
                    "Add",
                    new Type[] { mappingRule.TargetTypes[0] });

            // loop source values and add to collection
            foreach (var item in (IEnumerable)value)
            {
                var targetItem = mappingRule.GetTargetItemValue(item);

                addMethod.Invoke(
                    collection,
                    new object[] { targetItem });
            }

            // set target value
            mappingRule
                .TargetProperties[0]
                .SetValue(target, collection);
        }

        private static object GetTargetItemValue(
            this MappingRule mappingRule,
            object sourceItem)
        {
            if (mappingRule.Builder == null)
            {
                // return source value
                return sourceItem;
            }

            // use builder to get value from source value
            var froms = new object[] { sourceItem };

            return mappingRule
                .Builder
                .DynamicInvoke(froms);
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
