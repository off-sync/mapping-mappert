/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace OffSync.Mapping.Mappert.Tests.Models
{
    public class Shared
    {
        public int Id { get; set; }
    }

    public class SharedSub :
        Shared
    {
        public string Label { get; set; }
    }
}
