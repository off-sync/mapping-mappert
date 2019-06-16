/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

using OffSync.Mapping.Mappert.DynamicMethods.Common;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public class DynamicMethodMappingDelegateBuilder :
        IMappingDelegateBuilder
    {
        public static void SetAsDefault()
        {
            MappingDelegateBuilderRegistry.Default = new DynamicMethodMappingDelegateBuilder();
        }

        public MappingDelegate<TSource, TTarget> CreateMappingDelegate<TSource, TTarget>(
            IEnumerable<IMappingRule> mappingRules)
        {
            #region Pre-conditions
            if (mappingRules == null)
            {
                throw new ArgumentNullException(nameof(mappingRules));
            }

            if (mappingRules.Any(
                r => r.Builder == null &&
                (r.SourceProperties.Count != 1 ||
                r.TargetProperties.Count != 1)))
            {
                throw new ArgumentException(
                    Messages.BuilderMustBePresentForEveryMultiPropertyMappingRule,
                    nameof(mappingRules));
            }
            #endregion

            var mapMethod = new DynamicMethod(
                name: string.Format(
                    Constants.DynamicMapMethodName,
                    typeof(TSource).Name,
                    typeof(TTarget).Name),
                returnType: null, // void
                parameterTypes: new Type[]
                {
                    typeof(TSource),
                    typeof(TTarget),
                    typeof(Delegate[])
                });

            var builders = new List<Delegate>();

            using (var methodBuilder = new DynamicMethodBuilder(mapMethod))
            {
                foreach (var mappingRule in mappingRules)
                {
                    methodBuilder.AddMappingRule(mappingRule);

                    if (mappingRule.Builder != null)
                    {
                        builders.Add(mappingRule.Builder);
                    }
                }
            }

            var mapDelegate = (MapDelegate<TSource, TTarget>)mapMethod.CreateDelegate(
                typeof(MapDelegate<TSource, TTarget>));

            var mappingDelegate = new DynamicMethodMappingDelegate<TSource, TTarget>(
                mapDelegate,
                builders.ToArray());

            return mappingDelegate.Map;
        }
    }
}
