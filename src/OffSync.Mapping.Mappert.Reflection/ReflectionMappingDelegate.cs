/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Reflection.MappingSteps;

namespace OffSync.Mapping.Mappert.Reflection
{
    public sealed class ReflectionMappingDelegate<TSource, TTarget>
    {
        private readonly MappingStep[] _mappingSteps;

        internal ReflectionMappingDelegate(MappingStep[] mappingSteps)
        {
            _mappingSteps = mappingSteps;
        }

        internal void Map(
            TSource source,
            TTarget target)
        {
            for (int i = 0; i < _mappingSteps.Length; i++)
            {
                _mappingSteps[i].Apply(
                    source,
                    target);
            }
        }
    }
}
