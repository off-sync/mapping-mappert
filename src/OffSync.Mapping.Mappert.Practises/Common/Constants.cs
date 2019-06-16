/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace OffSync.Mapping.Mappert.Practises.Common
{
    static class Constants
    {
        /// <summary>
        /// Placeholder: number of tuple items.
        /// </summary>
#if NET461
        public const string ValueTupleTypeName = "System.ValueTuple`{0},System.ValueTuple"; // value tuple is defined in a separate assembly
#else
        public const string ValueTupleTypeName = "System.ValueTuple`{0}";
#endif
    }
}
