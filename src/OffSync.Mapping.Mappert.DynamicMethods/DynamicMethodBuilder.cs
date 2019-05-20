using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using GrEmit;

using OffSync.Mapping.Mappert.DynamicMethods.Common;
using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    sealed class DynamicMethodBuilder :
        IDisposable
    {
        #region Lookups
        private const int SourceArg = 0;

        private const int TargetArg = 1;

        private const int BuildersArg = 2;

        private static readonly MethodInfo GetTypeFromHandle = typeof(Type)
            .GetMethod(nameof(Type.GetTypeFromHandle));

        private static readonly MethodInfo GetItemsCount = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.GetItemsCount));

        private static readonly MethodInfo FillArray = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillArray));

        private static readonly MethodInfo FillArrayWithBuilder = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillArrayWithBuilder));

        private static readonly MethodInfo CreateCollection = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.CreateCollection));

        private static readonly MethodInfo FillCollection = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillCollection));

        private static readonly MethodInfo FillCollectionWithBuilder = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillCollectionWithBuilder));
        #endregion

        #region Internal state
        private readonly GroboIL il; // FIXME rename

        private readonly GroboIL.Local froms;

        private readonly GroboIL.Local value;

        private int builderIndex = 0;
        #endregion

        public DynamicMethodBuilder(
            DynamicMethod dynamicMethod)
        {
            il = new GroboIL(dynamicMethod);

            froms = il.DeclareLocal(
                typeof(object[]),
                nameof(froms));

            value = il.DeclareLocal(
                typeof(object),
                nameof(value));
        }

        public void Dispose()
        {
            il.Ret();

            il.Dispose();
        }

        internal void AddMappingRule(
            IMappingRule mappingRule)
        {
            if (mappingRule.Builder == null &&
                (mappingRule.SourceProperties.Count != 1 ||
                mappingRule.TargetProperties.Count != 1))
            {
                throw new ArgumentException(
                    $"no builder provided, exactly one source and target property must be provided",
                    nameof(mappingRule));
            }

            switch (mappingRule.Type)
            {
                case MappingRuleTypes.MapToValue:
                    if (mappingRule.Builder == null)
                    {
                        AddDirectAssignment(mappingRule);
                    }
                    else
                    {
                        AddApplyToValueBuilderAssignment(mappingRule);
                    }

                    break;

                case MappingRuleTypes.MapToArray:
                    AddApplyToArrayBuilderAssignment(mappingRule);

                    break;

                case MappingRuleTypes.MapToCollection:
                    AddApplyToCollectionBuilderAssignment(mappingRule);

                    break;
            }

            if (mappingRule.Builder != null)
            {
                // keep track of the current builder index
                builderIndex++;
            }
        }

        private void AddDirectAssignment(
            IMappingRule mappingRule)
        {
            il.Ldarg(TargetArg);

            il.Ldarg(SourceArg);

            il.Call(mappingRule.SourceProperties[0].GetMethod);

            if (mappingRule.SourceProperties[0].PropertyType.IsValueType)
            {
                il.Box(mappingRule.SourceProperties[0].PropertyType);
            }

            if (mappingRule.TargetProperties[0].PropertyType.IsValueType)
            {
                il.Unbox_Any(mappingRule.TargetProperties[0].PropertyType);
            }

            il.Call(mappingRule.TargetProperties[0].SetMethod);
        }

        private void AddApplyToValueBuilderAssignment(
            IMappingRule mappingRule)
        {
            var builderType = BuildersUtil.GetBuilderType(mappingRule);

            il.Ldc_I4(mappingRule.SourceProperties.Count);

            il.Newarr(typeof(object));

            il.Stloc(froms);

            for (int i = 0; i < mappingRule.SourceProperties.Count; i++)
            {
                il.Ldloc(froms);

                il.Ldc_I4(i);

                il.Ldarg(SourceArg);

                il.Call(mappingRule.SourceProperties[i].GetMethod); // source.SourceProperty i

                if (mappingRule.SourceProperties[i].PropertyType.IsValueType)
                {
                    // box the value type
                    il.Box(mappingRule.SourceProperties[i].PropertyType);
                }

                il.Stelem(typeof(object)); // froms[i] =
            }

            il.Ldarg(BuildersArg);

            il.Ldc_I4(builderIndex);

            il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

            il.Ldloc(froms);

            il.Call(
                typeof(Delegate)
                    .GetMethod(
                        nameof(Delegate.DynamicInvoke), // FIXME change to invoke
                        new Type[] { typeof(object[]) }));

            switch (builderType)
            {
                case BuilderTypes.SingleValue:
                    AddSingleValueAssignment(mappingRule.TargetProperties[0]);

                    break;

                case BuilderTypes.ValueTuple:
                    AddValueTupleAssignment(
                        mappingRule.Builder.Method.ReturnType,
                        mappingRule.TargetProperties.ToArray());

                    break;

                case BuilderTypes.ObjectArray:
                    AddObjectArrayAssignment(mappingRule.TargetProperties.ToArray());

                    break;
            }
        }

        private void AddSingleValueAssignment(
            PropertyInfo targetProperty)
        {
            il.Stloc(value);

            il.Ldarg(TargetArg);

            il.Ldloc(value);

            if (targetProperty.PropertyType.IsValueType)
            {
                il.Unbox_Any(targetProperty.PropertyType);
            }
            else
            {
                il.Castclass(targetProperty.PropertyType);
            }

            il.Call(targetProperty.SetMethod);
        }

        private void AddValueTupleAssignment(
            Type returnType,
            PropertyInfo[] targetProperties)
        {
            il.Unbox_Any(returnType);

            var valueTuple = il.DeclareLocal( // FIXME use lookup of locals by type
                returnType,
                "valueTuple");

            il.Stloc(valueTuple);

            for (int i = 0; i < targetProperties.Length; i++)
            {
                il.Ldarg(TargetArg);

                il.Ldloca(valueTuple); // load by ref required for GetField

                il.Ldfld(returnType.GetField($"Item{i + 1}"));

                il.Call(targetProperties[i].SetMethod);
            }
        }

        private void AddObjectArrayAssignment(
            PropertyInfo[] targetProperties)
        {
            il.Stloc(value);

            for (int i = 0; i < targetProperties.Length; i++)
            {
                il.Ldarg(TargetArg);

                il.Ldloc(value);

                il.Castclass(typeof(object[]));

                il.Ldc_I4(i);

                il.Ldelem(typeof(object)); // value[i]

                if (targetProperties[i].PropertyType.IsValueType)
                {
                    il.Unbox_Any(targetProperties[i].PropertyType);
                }
                else
                {
                    il.Castclass(targetProperties[i].PropertyType);
                }

                il.Call(targetProperties[i].SetMethod);
            }
        }

        private void AddApplyToArrayBuilderAssignment(
            IMappingRule mappingRule)
        {
            il.Ldarg(TargetArg);

            il.Ldarg(SourceArg);

            il.Call(mappingRule.SourceProperties[0].GetMethod);

            il.Stloc(value);

            il.Ldloc(value);

            il.Call(GetItemsCount);

            il.Newarr(mappingRule.TargetItemsType);

            il.Ldloc(value);

            il.Castclass(
                typeof(IEnumerable<>)
                    .MakeGenericType(mappingRule.SourceItemsType));

            if (mappingRule.Builder == null)
            {
                il.Call(FillArray
                    .MakeGenericMethod(
                        mappingRule.SourceItemsType));
            }
            else
            {
                il.Ldarg(BuildersArg);

                il.Ldc_I4(builderIndex);

                il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

                il.Castclass(
                    typeof(Func<,>)
                        .MakeGenericType(
                            mappingRule.SourceItemsType,
                            mappingRule.TargetItemsType));

                il.Call(FillArrayWithBuilder
                    .MakeGenericMethod(
                        mappingRule.SourceItemsType,
                        mappingRule.TargetItemsType));
            }

            il.Call(mappingRule.TargetProperties[0].SetMethod);
        }

        private void AddApplyToCollectionBuilderAssignment(
            IMappingRule mappingRule)
        {
            il.Ldarg(TargetArg);

            il.Ldtoken(mappingRule.TargetProperties[0].PropertyType);

            il.Call(GetTypeFromHandle);

            il.Ldtoken(mappingRule.TargetItemsType);

            il.Call(GetTypeFromHandle);

            il.Call(CreateCollection);

            il.Castclass(mappingRule.TargetProperties[0].PropertyType);

            il.Ldarg(SourceArg);

            il.Call(mappingRule.SourceProperties[0].GetMethod);

            il.Castclass(
                typeof(IEnumerable<>)
                    .MakeGenericType(mappingRule.SourceItemsType));

            if (mappingRule.Builder == null)
            {
                il.Call(FillCollection
                    .MakeGenericMethod(
                        mappingRule.SourceItemsType));
            }
            else
            {
                il.Ldarg(BuildersArg);

                il.Ldc_I4(builderIndex);

                il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

                il.Castclass(
                    typeof(Func<,>)
                        .MakeGenericType(
                            mappingRule.SourceItemsType,
                            mappingRule.TargetItemsType));

                il.Call(FillCollectionWithBuilder
                    .MakeGenericMethod(
                        mappingRule.SourceItemsType,
                        mappingRule.TargetItemsType));
            }

            il.Castclass(mappingRule.TargetProperties[0].PropertyType);

            il.Call(mappingRule.TargetProperties[0].SetMethod);
        }
    }
}
