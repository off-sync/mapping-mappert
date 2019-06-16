/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    public static class TestUtil
    {
        public static IEnumerable<T> Yield<T>(
            this T item)
        {
            yield return item;
        }
    }
}
