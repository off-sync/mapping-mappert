using System;
using System.Reflection;
using System.Reflection.Emit;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Interfaces;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public class DynamicMethodMappingDelegateBuilder :
        IMappingDelegateBuilder
    {
        public Delegate CreateMappingDelegate(
            Type sourceType,
            PropertyInfo[] sourceProperties,
            Type targetType,
            PropertyInfo[] targetProperties,
            Delegate builder)
        {
            if (builder == null &&
                (sourceProperties.Length != 1 ||
                targetProperties.Length != 1))
            {
                throw new ArgumentException(
                    $"no builder provided, exactly on source and target property must be provided",
                    nameof(builder));
            }

            // build dynamic method for this builder
            var mapping = new DynamicMethod(
                "Mapping",
                null,
                new Type[] { sourceType, targetType, typeof(Delegate) });

            var il = mapping.GetILGenerator();

            if (builder == null)
            {
                AddDirectAssignment(
                    il,
                    sourceProperties[0],
                    targetProperties[0]);
            }
            else
            {
                AddBuilderAssignment(
                    il,
                    sourceProperties,
                    targetProperties,
                    builder);
            }

            il.Emit(OpCodes.Ret);

            var mappingType = typeof(Action<,,>)
                .MakeGenericType(
                    sourceType,
                    targetType,
                    typeof(Delegate));

            return mapping.CreateDelegate(mappingType);
        }

        private void AddDirectAssignment(
            ILGenerator il,
            PropertyInfo sourceProperty,
            PropertyInfo targetProperty)
        {
            il.Emit(OpCodes.Ldarg_1); // target

            il.Emit(OpCodes.Ldarg_0); // source

            il.Emit(OpCodes.Callvirt, sourceProperty.GetMethod);

            if (sourceProperty.PropertyType.IsValueType)
            {
                // box the value type
                il.Emit(OpCodes.Box, sourceProperty.PropertyType);
            }

            if (targetProperty.PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, targetProperty.PropertyType);
            }

            il.Emit(OpCodes.Callvirt, targetProperty.SetMethod);
        }

        private void AddBuilderAssignment(
            ILGenerator il,
            PropertyInfo[] sourceProperties,
            PropertyInfo[] targetProperties,
            Delegate builder)
        {
            // check and get builder type
            var builderType = BuilderUtil.GetBuilderType(
                sourceProperties,
                targetProperties,
                builder);

            var froms = il.DeclareLocal(typeof(object[]));

            var value = il.DeclareLocal(typeof(object));

            il.Emit(OpCodes.Ldc_I4, sourceProperties.Length);

            il.Emit(OpCodes.Newarr, typeof(object));

            il.Emit(OpCodes.Stloc, froms);

            for (int i = 0; i < sourceProperties.Length; i++)
            {
                il.Emit(OpCodes.Ldloc, froms);

                il.Emit(OpCodes.Ldc_I4, i);

                il.Emit(OpCodes.Ldarg_0); // source

                il.Emit(OpCodes.Callvirt, sourceProperties[i].GetMethod); // source.SourceProperty i

                if (sourceProperties[i].PropertyType.IsValueType)
                {
                    // box the value type
                    il.Emit(OpCodes.Box, sourceProperties[i].PropertyType);
                }

                il.Emit(OpCodes.Stelem_Ref); // froms[i] =
            }

            il.Emit(OpCodes.Ldarg_2);

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
                        targetProperties[0]);

                    break;

                case BuilderTypes.ValueTuple:
                    AddValueTupleAssignment(
                        il,
                        builder.Method.ReturnType,
                        value,
                        targetProperties);

                    break;

                case BuilderTypes.ObjectArray:
                    AddObjectArrayAssignment(
                        il,
                        value,
                        targetProperties);

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
