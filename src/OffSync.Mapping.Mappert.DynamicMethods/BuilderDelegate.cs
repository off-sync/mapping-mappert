/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public delegate TTarget BuilderDelegate<in TSource, out TTarget>(
        TSource source);
}
