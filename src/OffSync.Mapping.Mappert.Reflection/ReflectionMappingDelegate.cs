using System;

using OffSync.Mapping.Mappert.Reflection.MappingSteps;

namespace OffSync.Mapping.Mappert.Reflection
{
    public sealed class ReflectionMappingDelegate<TSource, TTarget>
    {
        private readonly MappingStep[] _mappingSteps;

        public ReflectionMappingDelegate(MappingStep[] mappingSteps)
        {
            #region Pre-conditions
            if (mappingSteps == null)
            {
                throw new ArgumentNullException(nameof(mappingSteps));
            }
            #endregion

            this._mappingSteps = mappingSteps;
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
