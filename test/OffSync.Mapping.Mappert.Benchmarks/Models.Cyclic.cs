/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class CyclicSourceModel
    {
        public static CyclicSourceModel Create()
        {
            var model = new CyclicSourceModel();

            var child = new CyclicSourceModel()
            {
                Parent = model,
            };

            var grantChild = new CyclicSourceModel()
            {
                Parent = child,
            };

            child.Children = new CyclicSourceModel[]
            {
                grantChild,
            };

            model.Children = new CyclicSourceModel[]
            {
                child,
            };

            return model;
        }

        public CyclicSourceModel Parent { get; set; }

        public CyclicSourceModel[] Children { get; set; }
    }

    public class CyclicTargetModel
    {
        public CyclicTargetModel Parent { get; set; }

        public CyclicTargetModel[] Children { get; set; }
    }

    public class CyclicMapper :
        Mapper<CyclicSourceModel, CyclicTargetModel>
    {
        public CyclicMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);
        }
    }
}