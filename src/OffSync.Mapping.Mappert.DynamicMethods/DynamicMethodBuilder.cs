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

        private static readonly MethodInfo GetItemsCount = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.GetItemsCount));

        private static readonly MethodInfo FillArray = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillArray));

        private static readonly MethodInfo FillArrayWithBuilder = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillArrayWithBuilder));

        private static readonly MethodInfo FillCollection = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillCollection));

        private static readonly MethodInfo FillCollectionWithBuilder = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillCollectionWithBuilder));

        private static readonly MethodInfo FillList = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillList));

        private static readonly MethodInfo FillListWithBuilder = typeof(ItemsUtil)
            .GetMethod(nameof(ItemsUtil.FillListWithBuilder));
        #endregion

        #region Internal state
        private readonly GroboIL _il;

        private readonly IDictionary<Type, GroboIL.Local> _typedLocals = new Dictionary<Type, GroboIL.Local>();

        private int builderIndex = 0;
        #endregion

        public DynamicMethodBuilder(
            DynamicMethod dynamicMethod)
        {
            _il = new GroboIL(dynamicMethod);
        }

        public void Dispose()
        {
            _il.Ret();

            _il.Dispose();

#if DEBUG
            System.Console.Out.WriteLine(_il.GetILCode());
#endif
        }

        private GroboIL.Local GetSharedTypedLocal(
            Type type)
        {
            if (_typedLocals.TryGetValue(
                type,
                out var local))
            {
                return local;
            }

            local = _il.DeclareLocal(
                type,
                type.Name);

            _typedLocals[type] = local;

            return local;
        }

        internal void AddMappingRule(
            IMappingRule mappingRule)
        {
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

        private Action<GroboIL> AddValueNullCheck(
            PropertyInfo property,
            int stackSize)
        {
            if (property
                .PropertyType
                .IsValueType)
            {
                // null checks are not required for value types
                return (il) => { };
            }

            _il.Dup();

            var nonNull = _il.DefineLabel(
                string.Format(
                    Constants.PropertyNameIsNotNull,
                    property.Name));

            _il.Brtrue(nonNull); // set value if value is non-null

            // value is null: pop everything from the stack
            for (int i = 0; i < stackSize; i++)
            {
                _il.Pop();
            }

            var end = _il.DefineLabel(
                string.Format(
                    Constants.PropertyNameNullCheckEnd,
                    property.Name));

            _il.Br(end); // go to end

            _il.MarkLabel(nonNull);

            // return a callback to insert the end label at the right place
            return (il) =>
            {
                il.MarkLabel(end);
            };
        }

        private void AddDirectAssignment(
            IMappingRule mappingRule)
        {
            _il.Ldarg(TargetArg);

            _il.Ldarg(SourceArg);

            _il.Call(mappingRule.SourceProperties[0].GetMethod);

            var markEnd = AddValueNullCheck(
                mappingRule.SourceProperties[0],
                stackSize: 2); // value, target

            _il.Call(mappingRule.TargetProperties[0].SetMethod);

            markEnd(_il);
        }

        private void AddApplyToValueBuilderAssignment(
            IMappingRule mappingRule)
        {
            var builderType = BuildersUtil.GetBuilderType(mappingRule);

            _il.Ldarg(BuildersArg);

            _il.Ldc_I4(builderIndex);

            _il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

            _il.Castclass(mappingRule
                .Builder
                .GetType());

            Action<GroboIL> markEnd;

            if (mappingRule.SourceProperties.Count == 1)
            {
                // only add a null check if there is a single source property
                _il.Ldarg(SourceArg);

                _il.Call(mappingRule.SourceProperties[0].GetMethod);

                markEnd = AddValueNullCheck(
                    mappingRule.SourceProperties[0],
                    stackSize: 2); // value, builder
            }
            else
            {
                for (int i = 0; i < mappingRule.SourceProperties.Count; i++)
                {
                    _il.Ldarg(SourceArg);

                    _il.Call(mappingRule.SourceProperties[i].GetMethod);
                }

                markEnd = (il) => { };
            }

            var types = mappingRule
                .SourceProperties
                .Select(pi => pi.PropertyType)
                .ToArray();

            _il.Call(
                mappingRule
                    .Builder
                    .GetType()
                    .GetMethod(
                        Constants.InvokeMethodName,
                        types));

            switch (builderType)
            {
                case BuilderTypes.SingleValue:
                    AddSingleValueAssignment(
                        mappingRule.Builder.Method.ReturnType,
                        mappingRule.TargetProperties[0]);

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

            markEnd(_il);
        }

        private void AddSingleValueAssignment(
            Type returnType,
            PropertyInfo targetProperty)
        {
            var value = GetSharedTypedLocal(returnType);

            _il.Stloc(value);

            _il.Ldarg(TargetArg);

            _il.Ldloc(value);

            _il.Call(targetProperty.SetMethod);
        }

        private void AddValueTupleAssignment(
            Type returnType,
            PropertyInfo[] targetProperties)
        {
            var value = GetSharedTypedLocal(returnType);

            _il.Stloc(value);

            for (int i = 0; i < targetProperties.Length; i++)
            {
                _il.Ldarg(TargetArg);

                _il.Ldloca(value); // load by ref required for GetField on ValueTuple

                _il.Ldfld(
                    returnType.GetField(
                        string.Format(
                            Constants.ItemFieldName,
                            i + 1)));

                _il.Call(targetProperties[i].SetMethod);
            }
        }

        private void AddObjectArrayAssignment(
            PropertyInfo[] targetProperties)
        {
            var value = GetSharedTypedLocal(typeof(object[]));

            _il.Stloc(value);

            for (int i = 0; i < targetProperties.Length; i++)
            {
                _il.Ldarg(TargetArg);

                _il.Ldloc(value);

                _il.Ldc_I4(i);

                _il.Ldelem(typeof(object)); // value[i]

                if (targetProperties[i].PropertyType.IsValueType)
                {
                    _il.Unbox_Any(targetProperties[i].PropertyType);
                }
                else
                {
                    _il.Castclass(targetProperties[i].PropertyType);
                }

                _il.Call(targetProperties[i].SetMethod);
            }
        }

        private void AddApplyToArrayBuilderAssignment(
            IMappingRule mappingRule)
        {
            _il.Ldarg(TargetArg);

            _il.Ldarg(SourceArg);

            _il.Call(mappingRule.SourceProperties[0].GetMethod);

            var markEnd = AddValueNullCheck(
                mappingRule.SourceProperties[0],
                stackSize: 2); // value, target

            var value = GetSharedTypedLocal(mappingRule.SourceProperties[0].PropertyType);

            _il.Stloc(value);

            _il.Ldloc(value);

            _il.Call(GetItemsCount
                .MakeGenericMethod(
                    mappingRule.SourceItemsType));

            _il.Newarr(mappingRule.TargetItemsType);

            _il.Ldloc(value);

            if (mappingRule.Builder == null)
            {
                _il.Call(FillArray
                    .MakeGenericMethod(
                        mappingRule.SourceItemsType));
            }
            else
            {
                _il.Ldarg(BuildersArg);

                _il.Ldc_I4(builderIndex);

                _il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

                _il.Castclass(mappingRule
                    .Builder
                    .GetType());

                _il.Call(FillArrayWithBuilder
                    .MakeGenericMethod(
                        mappingRule.SourceItemsType,
                        mappingRule.TargetItemsType));
            }

            _il.Call(mappingRule.TargetProperties[0].SetMethod);

            markEnd(_il);
        }

        private void AddApplyToCollectionBuilderAssignment(
            IMappingRule mappingRule)
        {
            _il.Ldarg(TargetArg);

            ConstructorInfo constructor;

            MethodInfo fillMethod;

            if (mappingRule.TargetProperties[0].PropertyType.IsClass)
            {
                // use concrete class
                constructor = mappingRule
                    .TargetProperties[0]
                    .PropertyType
                    .GetConstructor(Type.EmptyTypes);

                if (mappingRule.Builder == null)
                {
                    fillMethod = FillCollection
                        .MakeGenericMethod(
                            mappingRule.TargetProperties[0].PropertyType,
                            mappingRule.SourceItemsType);
                }
                else
                {
                    fillMethod = FillCollectionWithBuilder
                        .MakeGenericMethod(
                            mappingRule.TargetProperties[0].PropertyType,
                            mappingRule.SourceItemsType,
                            mappingRule.TargetItemsType);
                }
            }
            else
            {
                // use a generic list
                constructor = typeof(List<>)
                    .MakeGenericType(mappingRule.TargetItemsType)
                    .GetConstructor(Type.EmptyTypes);

                if (mappingRule.Builder == null)
                {
                    fillMethod = FillList
                        .MakeGenericMethod(
                            mappingRule.SourceItemsType);
                }
                else
                {
                    fillMethod = FillListWithBuilder
                        .MakeGenericMethod(
                            mappingRule.SourceItemsType,
                            mappingRule.TargetItemsType);
                }
            }

            _il.Newobj(constructor);

            _il.Ldarg(SourceArg);

            _il.Call(mappingRule.SourceProperties[0].GetMethod);

            var markEnd = AddValueNullCheck(
                mappingRule.SourceProperties[0],
                stackSize: 3); // value, collection, target

            if (mappingRule.Builder != null)
            {
                _il.Ldarg(BuildersArg);

                _il.Ldc_I4(builderIndex);

                _il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

                _il.Castclass(mappingRule
                    .Builder
                    .GetType());
            }

            _il.Call(fillMethod);

            _il.Call(mappingRule.TargetProperties[0].SetMethod);

            markEnd(_il);
        }
    }
}
