/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

using OffSync.Mapping.Mappert.MapperBuilders;

namespace OffSync.Mapping.Mappert
{
    public class Mapper<TSource, TTarget> :
        AbstractMapper<TSource, TTarget>
        where TTarget : new()
    {
        public Mapper()
        {
        }

        public Mapper(
            Action<IMapperBuilder<TSource, TTarget>> configure) :
            base(configure)
        {
        }

        override protected TTarget CreateTarget()
        {
            return new TTarget();
        }
    }
}
