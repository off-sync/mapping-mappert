using System;
using System.Collections;
using System.Linq;

using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Reflection.Common;

namespace OffSync.Mapping.Mappert.Reflection.MappingSteps
{
    public static partial class MappingStepsExtensions
    {
        public static void Apply<TSource, TTarget>(
            this MappingStep mappingStep,
            TSource source,
            TTarget target)
        {
            switch (mappingStep.MappingRuleType)
            {
                case MappingRuleTypes.MapToValue:
                    mappingStep.ApplyToValue(
                        source,
                        target);

                    return;

                case MappingRuleTypes.MapToArray:
                    mappingStep.ApplyToArray(
                        source,
                        target);

                    return;

                case MappingRuleTypes.MapToCollection:
                    mappingStep.ApplyToCollection(
                        source,
                        target);

                    return;
            }
        }

        private static void ApplyToValue<TSource, TTarget>(
            this MappingStep mappingStep,
            TSource source,
            TTarget target)
        {
            object value;

            if (mappingStep.Builder == null)
            {
                // get value from single source property
                value = mappingStep
                    .SourceProperties[0]
                    .GetValue(source);

                if (value == null)
                {
                    // ignore this mapping
                    return;
                }
            }
            else
            {
                // use builder to get value from source properties
                var froms = mappingStep
                    .SourceProperties
                    .Select(pi => pi.GetValue(source))
                    .ToArray();

                if (froms.Length == 1 &&
                    froms[0] == null)
                {
                    // ignore this mapping
                    return;
                }

                value = mappingStep.Build(froms);
            }

            switch (mappingStep.BuilderType)
            {
                case BuilderTypes.ValueTuple:
                    // multi-valued assignment using ValueTuple
                    mappingStep.ApplyFromValueTuple(
                        target,
                        value);

                    return;

                case BuilderTypes.ObjectArray:
                    // multi-valued assignment using object array
                    mappingStep.ApplyFromArray(
                        target,
                        (object[])value);

                    return;

                // BuilderTypes.NoBuilder:
                // BuilderTypes.SingleValue:
                default:
                    // set value of single target property
                    mappingStep
                        .TargetProperties[0]
                        .SetValue(target, value);

                    return;
            }
        }

        private static void ApplyToArray<TSource, TTarget>(
            this MappingStep mappingStep,
            TSource source,
            TTarget target)
        {
            // get source value
            var value = mappingStep
                .SourceProperties[0]
                .GetValue(source);

            if (value == null)
            {
                // ignore this mapping
                return;
            }

            // get length of new array
            var length = ItemsUtil.GetItemsCount(value);

            // create new array
            var array = ItemsUtil.CreateArray(
                mappingStep.TargetItemType,
                length);

            // loop source values and set to array
            var i = 0;

            foreach (var item in (IEnumerable)value)
            {
                var targetItem = mappingStep.GetTargetItemValue(item);

                array.SetValue(
                    targetItem,
                    i++);
            }

            // set target value
            mappingStep
                .TargetProperties[0]
                .SetValue(target, array);
        }

        private static void ApplyToCollection<TSource, TTarget>(
            this MappingStep mappingStep,
            TSource source,
            TTarget target)
        {
            // get source value
            var value = mappingStep
                .SourceProperties[0]
                .GetValue(source);

            if (value == null)
            {
                // ignore this mapping
                return;
            }

            // create new collection
            var collection = ItemsUtil.CreateCollection(
                mappingStep.TargetProperties[0].PropertyType,
                mappingStep.TargetItemType);

            var addMethod = collection
                .GetType()
                .GetMethod(
                    Constants.AddMethodName,
                    new Type[] { mappingStep.TargetItemType });

            // loop source values and add to collection
            foreach (var item in (IEnumerable)value)
            {
                var targetItem = mappingStep.GetTargetItemValue(item);

                addMethod.Invoke(
                    collection,
                    new object[] { targetItem });
            }

            // set target value
            mappingStep
                .TargetProperties[0]
                .SetValue(target, collection);
        }

        private static object GetTargetItemValue(
            this MappingStep mappingStep,
            object sourceItem)
        {
            if (mappingStep.Builder == null)
            {
                // return source value
                return sourceItem;
            }

            // use builder to get value from source value
            var froms = new object[] { sourceItem };

            return mappingStep.Build(froms);
        }

        private static void ApplyFromValueTuple<TTarget>(
            this MappingStep mappingStep,
            TTarget target,
            object value)
        {
            for (int i = 0; i < mappingStep.TargetProperties.Length; i++)
            {
                var item = mappingStep
                    .ValueTupleFields[i]
                    .GetValue(value);

                mappingStep
                    .TargetProperties[i]
                    .SetValue(
                        target,
                        item);
            }
        }

        private static void ApplyFromArray<TTarget>(
            this MappingStep mappingStep,
            TTarget target,
            object[] value)
        {
            if (mappingStep.TargetProperties.Length != value.Length)
            {
                throw new InvalidOperationException(
                    string.Format(
                        Messages.InvalidNumberOfObjectsInValue,
                        value.Length,
                        mappingStep.TargetProperties.Length));
            }

            for (int i = 0; i < mappingStep.TargetProperties.Length; i++)
            {
                mappingStep
                    .TargetProperties[i]
                    .SetValue(
                        target,
                        value[i]);
            }
        }
    }
}
