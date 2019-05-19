using System;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public class DynamicMethodMappingDelegate<TSource, TTarget>
    {
        private readonly MapDelegate<TSource, TTarget> _mapDelegate;

        private readonly Delegate[] _builders;

        public DynamicMethodMappingDelegate(
            MapDelegate<TSource, TTarget> mapDelegate,
            Delegate[] builders)
        {
            #region Pre-conditions
            if (mapDelegate == null)
            {
                throw new ArgumentNullException(nameof(mapDelegate));
            }

            if (builders == null)
            {
                throw new ArgumentNullException(nameof(builders));
            }
            #endregion

            _mapDelegate = mapDelegate;

            _builders = builders;
        }

        internal void Map(
            TSource source,
            TTarget target)
        {
            _mapDelegate(
                source,
                target,
                _builders);
        }
    }
}
