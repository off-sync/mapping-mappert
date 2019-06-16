/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Reflection.MappingSteps;

namespace OffSync.Mapping.Mappert.Reflection
{
    public sealed class ReflectionMappingDelegateBuilder :
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

            var mappingSteps = mappingRules
                .Select(MappingStepsUtil.Create)
                .ToArray();

            var mappingDelegate = new ReflectionMappingDelegate<TSource, TTarget>(mappingSteps);

            return mappingDelegate.Map;
        }
    }
}
