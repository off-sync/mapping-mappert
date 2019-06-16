/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public delegate void MapDelegate<in TSource, in TTarget>(
        TSource source,
        TTarget target,
        Delegate[] builders);
}
