using System;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public delegate void MapDelegate<in TSource, in TTarget>(
        TSource source,
        TTarget target,
        Delegate[] builders);
}
