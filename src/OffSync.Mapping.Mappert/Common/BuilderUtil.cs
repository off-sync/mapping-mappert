using System;
using System.Linq;
using System.Reflection;

namespace OffSync.Mapping.Mappert.Common
{
    public static class BuilderUtil
    {
        public static BuilderTypes GetBuilderType(
            PropertyInfo[] sourceProperties,
            PropertyInfo[] targetProperties,
            Delegate builder)
        {
            #region Pre-conditions
            if (sourceProperties == null)
            {
                throw new ArgumentNullException(nameof(sourceProperties));
            }

            if (targetProperties == null)
            {
                throw new ArgumentNullException(nameof(targetProperties));
            }

            if (targetProperties.Length == 0)
            {
                throw new ArgumentException(
                    $"{nameof(targetProperties)} must contain at least one element",
                    nameof(targetProperties));
            }

            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            #endregion

            var paramTypes = builder.Method.GetParameters();

            if (paramTypes.Length != sourceProperties.Length)
            {
                throw new ArgumentException(
                    $"invalid number of builder parameters '{paramTypes.Length}', expected '{sourceProperties.Length}'");
            }

            for (int i = 0; i < sourceProperties.Length; i++)
            {
                if (!paramTypes[i].ParameterType.IsAssignableFrom(sourceProperties[i].PropertyType))
                {
                    throw new ArgumentException(
                        $"invalid builder parameter at index '{i}': '{paramTypes[i].ParameterType.FullName}' must be assignable from '{sourceProperties[i].PropertyType.FullName}'");
                }
            }

            var returnType = builder.Method.ReturnType;

            if (targetProperties.Length == 1)
            {
                if (!targetProperties[0].PropertyType.IsAssignableFrom(returnType))
                {
                    throw new ArgumentException(
                        $"invalid builder return type: '{targetProperties[0].PropertyType.FullName}' must be assignable from '{returnType.FullName}'");
                }

                return BuilderTypes.SingleValue;
            }

            // check if return type implements matching value tuple
            var valueTupleType = Type
                .GetType($"System.ValueTuple`{targetProperties.Length}")
                .MakeGenericType(
                    targetProperties
                        .Select(pi => pi.PropertyType)
                        .ToArray());

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
