/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;

using OffSync.Mapping.Mappert.Practises.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises.Builders
{
    public static class BuildersUtil
    {
        public static bool TryGetBuilderType(
            IMappingRule mappingRule,
            out BuilderTypes builderType,
            out Exception exception)
        {
            try
            {
                builderType = GetBuilderType(mappingRule);

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                builderType = BuilderTypes.NoBuilder;

                exception = ex;

                return false;
            }
        }

        public static BuilderTypes GetBuilderType(
            IMappingRule mappingRule)
        {
            #region Pre-conditions
            if (mappingRule == null)
            {
                throw new ArgumentNullException(nameof(mappingRule));
            }

            if (mappingRule.TargetProperties.Count == 0)
            {
                throw new ArgumentException(
                    Messages.TargetPropertiesMustContainAtLeastOneElement,
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
                    string.Format(
                        Messages.InvalidNumberOfBuilderParameters,
                        paramTypes.Length,
                        sourceTypes.Length));
            }

            for (int i = 0; i < sourceTypes.Length; i++)
            {
                if (!paramTypes[i].ParameterType.IsAssignableFrom(sourceTypes[i]))
                {
                    throw new ArgumentException(
                        string.Format(
                            Messages.InvalidBuilderParameterMustBeAssignable,
                            i,
                            paramTypes[i].ParameterType.FullName,
                            sourceTypes[i].FullName));
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
                        string.Format(
                            Messages.InvalidBuilderReturnTypeMustBeAssignable,
                            targetTypes[0].FullName,
                            returnType.FullName));
                }

                return BuilderTypes.SingleValue;
            }

            // check if return type implements matching value tuple
            var valueTupleTypeName = string.Format(
                Constants.ValueTupleTypeName,
                targetTypes.Length);

            var valueTupleType = Type
                .GetType(valueTupleTypeName)
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
                string.Format(
                    Messages.InvalidBuilderReturnTypeMustImplementValueTupleOrObjectArray,
                    returnType.FullName));
        }
    }
}
