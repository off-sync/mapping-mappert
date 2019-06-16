/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class EmptySourceModel
    {
    }

    public class EmptyTargetModel
    {
    }

    public class EmptyMapper :
        Mapper<EmptySourceModel, EmptyTargetModel>
    {
        public EmptyMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);
        }
    }
}