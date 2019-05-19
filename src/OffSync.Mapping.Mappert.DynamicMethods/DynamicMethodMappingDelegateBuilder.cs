using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using GrEmit;

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

            var builders = new List<Delegate>();

            using (var il = new GroboIL(mapMethod))
            {
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

                il.Ret();
            }

            var mapDelegate = (MapDelegate<TSource, TTarget>)mapMethod.CreateDelegate(
                typeof(MapDelegate<TSource, TTarget>));

            var mappingDelegate = new DynamicMethodMappingDelegate<TSource, TTarget>(
                mapDelegate,
                builders.ToArray());

            return mappingDelegate.Map;
        }

        private void AddMappingRule(
            GroboIL il,
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
            GroboIL il,
            IMappingRule mappingRule)
        {
            il.Ldarg(1); // target

            il.Ldarg(0); // source

            il.Call(mappingRule.SourceProperties[0].GetMethod);

            if (mappingRule.SourceProperties[0].PropertyType.IsValueType)
            {
                // box the value type
                il.Box(mappingRule.SourceProperties[0].PropertyType);
            }

            if (mappingRule.TargetProperties[0].PropertyType.IsValueType)
            {
                il.Unbox_Any(mappingRule.TargetProperties[0].PropertyType);
            }

            il.Call(mappingRule.TargetProperties[0].SetMethod);
        }

        private void AddBuilderAssignment(
            GroboIL il,
            IMappingRule mappingRule,
            int builderIndex)
        {
            var builderType = BuildersUtil.GetBuilderType(mappingRule);

            var froms = il.DeclareLocal(typeof(object[]));

            var value = il.DeclareLocal(typeof(object));

            il.Ldc_I4(mappingRule.SourceProperties.Count);

            il.Newarr(typeof(object));

            il.Stloc(froms);

            for (int i = 0; i < mappingRule.SourceProperties.Count; i++)
            {
                il.Ldloc(froms);

                il.Ldc_I4(i);

                il.Ldarg(0); // source

                il.Call(mappingRule.SourceProperties[i].GetMethod); // source.SourceProperty i

                if (mappingRule.SourceProperties[i].PropertyType.IsValueType)
                {
                    // box the value type
                    il.Box(mappingRule.SourceProperties[i].PropertyType);
                }

                il.Stelem(typeof(object)); // froms[i] =
            }

            il.Ldarg(2); // Builders

            il.Ldc_I4(builderIndex);

            il.Ldelem(typeof(Delegate)); // Builders[builderIndex]

            il.Ldloc(froms);

            il.Call(
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
            GroboIL il,
            GroboIL.Local value,
            PropertyInfo targetProperty)
        {
            // assign builder value directly
            il.Stloc(value);

            il.Ldarg(1); // target

            il.Ldloc(value);

            if (targetProperty.PropertyType.IsValueType)
            {
                il.Unbox_Any(targetProperty.PropertyType);
            }

            il.Call(targetProperty.SetMethod); // target.TargetProperty0 =
        }

        private void AddValueTupleAssignment(
            GroboIL il,
            Type returnType,
            GroboIL.Local value,
            PropertyInfo[] targetProperties)
        {
            il.Unbox_Any(returnType);

            var valueTuple = il.DeclareLocal(returnType);

            il.Stloc(valueTuple);

            // builder returns a value tuple
            for (int i = 0; i < targetProperties.Length; i++)
            {
                il.Ldarg(1); // target

                il.Ldloca(valueTuple); // load by ref

                il.Ldfld(returnType.GetField($"Item{i + 1}")); // ValueTuple.Item i+1

                il.Call(targetProperties[i].SetMethod); // target.TargetProperty i =
            }
        }

        private void AddObjectArrayAssignment(
            GroboIL il,
            GroboIL.Local value,
            PropertyInfo[] targetProperties)
        {
            il.Stloc(value);

            // builder returns a value tuple
            for (int i = 0; i < targetProperties.Length; i++)
            {
                il.Ldarg(1); // target

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

                il.Call(targetProperties[i].SetMethod); // target.TargetProperty i =
            }
        }
    }
}
