/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public class DynamicMethodMappingDelegate<TSource, TTarget>
    {
        private readonly MapDelegate<TSource, TTarget> _mapDelegate;

        private readonly Delegate[] _builders;

        internal DynamicMethodMappingDelegate(
            MapDelegate<TSource, TTarget> mapDelegate,
            Delegate[] builders)
        {
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
