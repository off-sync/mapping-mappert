/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace OffSync.Mapping.Mappert.Practises
{
    public delegate void MappingDelegate<in TSource, in TTarget>(
        TSource source,
        TTarget target);
}
