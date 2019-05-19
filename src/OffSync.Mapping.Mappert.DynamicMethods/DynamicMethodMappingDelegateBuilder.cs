using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public class DynamicMethodMappingDelegateBuilder :
        IMappingDelegateBuilder
    {
        public MappingDelegate<TSource, TTarget> CreateMappingDelegate<TSource, TTarget>(
            IEnumerable<IMappingRule> mappingRules)
        {
            #region Pre-conditions
            if (mappingRules == null)
            {
                throw new ArgumentNullException(nameof(mappingRules));
            }
            #endregion

            var mapMethod = new DynamicMethod(
                $"Map{typeof(TSource).Name}To{typeof(TTarget).Name}",
                null,
                new Type[] { typeof(TSource), typeof(TTarget), typeof(Delegate[]) });

            var il = mapMethod.GetILGenerator();

            var builders = new List<Delegate>();

            foreach (var mappingRule in mappingRules)
            {
                AddMappingRule(
                    il,
                    mappingRule,
                    builders.Count);

                if (mappingRule.Builder != null)
                {
                    builders.Add(mappingRule.Builder);
                }
            }

            il.Emit(OpCodes.Ret);

            var mapDelegate = (MapDelegate<TSource, TTarget>)mapMethod.CreateDelegate(
                typeof(MapDelegate<TSource, TTarget>));

            var mappingDelegate = new DynamicMethodMappingDelegate<TSource, TTarget>(
                mapDelegate,
                builders.ToArray());

            return mappingDelegate.Map;
        }

        private void AddMappingRule(
            ILGenerator il,
            IMappingRule mappingRule,
            int builderIndex)
        {
            if (mappingRule.Builder == null &&
                (mappingRule.SourceProperties.Count != 1 ||
                mappingRule.TargetProperties.Count != 1))
            {
                throw new ArgumentException(
                    $"no builder provided, exactly one source and target property must be provided",
                    nameof(mappingRule));
            }

            if (mappingRule.Builder == null)
            {
                AddDirectAssignment(
                    il,
                    mappingRule);
            }
            else
            {
                AddBuilderAssignment(
                    il,
                    mappingRule,
                    builderIndex);
            }
        }

        private void AddDirectAssignment(
            ILGenerator il,
            IMappingRule mappingRule)
        {
            il.Emit(OpCodes.Ldarg_1); // target

            il.Emit(OpCodes.Ldarg_0); // source

            il.Emit(OpCodes.Callvirt, mappingRule.SourceProperties[0].GetMethod);

            if (mappingRule.SourceProperties[0].PropertyType.IsValueType)
            {
                // box the value type
                il.Emit(OpCodes.Box, mappingRule.SourceProperties[0].PropertyType);
            }

            if (mappingRule.TargetProperties[0].PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, mappingRule.TargetProperties[0].PropertyType);
            }

            il.Emit(OpCodes.Callvirt, mappingRule.TargetProperties[0].SetMethod);
        }

        private void AddBuilderAssignment(
            ILGenerator il,
            IMappingRule mappingRule,
            int builderIndex)
        {
            var builderType = BuildersUtil.GetBuilderType(mappingRule);

            var froms = il.DeclareLocal(typeof(object[]));

            var value = il.DeclareLocal(typeof(object));

            il.Emit(OpCodes.Ldc_I4, mappingRule.SourceProperties.Count);

            il.Emit(OpCodes.Newarr, typeof(object));

            il.Emit(OpCodes.Stloc, froms);

            for (int i = 0; i < mappingRule.SourceProperties.Count; i++)
            {
                il.Emit(OpCodes.Ldloc, froms);

                il.Emit(OpCodes.Ldc_I4, i);

                il.Emit(OpCodes.Ldarg_0); // source

                il.Emit(OpCodes.Callvirt, mappingRule.SourceProperties[i].GetMethod); // source.SourceProperty i

                if (mappingRule.SourceProperties[i].PropertyType.IsValueType)
                {
                    // box the value type
                    il.Emit(OpCodes.Box, mappingRule.SourceProperties[i].PropertyType);
                }

                il.Emit(OpCodes.Stelem_Ref); // froms[i] =
            }

            il.Emit(OpCodes.Ldarg_2); // Builders

            il.Emit(OpCodes.Ldc_I4, builderIndex);

            il.Emit(OpCodes.Ldelem_Ref); // Builders[builderIndex]

            il.Emit(OpCodes.Ldloc, froms);

            il.Emit(
                OpCodes.Callvirt,
                typeof(Delegate)
                    .GetMethod(
                        nameof(Delegate.DynamicInvoke),
                        new Type[] { typeof(object[]) }));

            switch (builderType)
            {
                case BuilderTypes.SingleValue:
                    AddSingleValueAssignment(
                        il,
                        value,
                        mappingRule.TargetProperties[0]);

                    break;

                case BuilderTypes.ValueTuple:
                    AddValueTupleAssignment(
                        il,
                        mappingRule.Builder.Method.ReturnType,
                        value,
                        mappingRule.TargetProperties.ToArray());

                    break;

                case BuilderTypes.ObjectArray:
                    AddObjectArrayAssignment(
                        il,
                        value,
                        mappingRule.TargetProperties.ToArray());

                    break;
            }
        }

        private void AddSingleValueAssignment(
            ILGenerator il,
            LocalBuilder value,
            PropertyInfo targetProperty)
        {
            // assign builder value directly
            il.Emit(OpCodes.Stloc, value);

            il.Emit(OpCodes.Ldarg_1); // target

            il.Emit(OpCodes.Ldloc, value);

            if (targetProperty.PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, targetProperty.PropertyType);
            }

            il.Emit(OpCodes.Callvirt, targetProperty.SetMethod); // target.TargetProperty0 =
        }

        private void AddValueTupleAssignment(
            ILGenerator il,
            Type returnType,
            LocalBuilder value,
            PropertyInfo[] targetProperties)
        {
            il.Emit(OpCodes.Unbox_Any, returnType);

            il.Emit(OpCodes.Stloc, value);

            // builder returns a value tuple
            for (int i = 0; i < targetProperties.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1); // target

                il.Emit(OpCodes.Ldloc, value);

                il.Emit(OpCodes.Ldfld, returnType.GetField($"Item{i + 1}")); // ValueTuple.Item i+1

                il.Emit(OpCodes.Callvirt, targetProperties[i].SetMethod); // target.TargetProperty i =
            }
        }

        private void AddObjectArrayAssignment(
            ILGenerator il,
            LocalBuilder value,
            PropertyInfo[] targetProperties)
        {
            il.Emit(OpCodes.Stloc, value);

            // builder returns a value tuple
            for (int i = 0; i < targetProperties.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1); // target

                il.Emit(OpCodes.Ldloc, value);

                il.Emit(OpCodes.Ldc_I4, i);

                il.Emit(OpCodes.Ldelem_Ref); // value[i]

                if (targetProperties[i].PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Unbox_Any, targetProperties[i].PropertyType);
                }

                il.Emit(OpCodes.Callvirt, targetProperties[i].SetMethod); // target.TargetProperty i =
            }
        }
    }
}
