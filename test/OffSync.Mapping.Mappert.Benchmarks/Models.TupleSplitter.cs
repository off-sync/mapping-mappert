﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class TupleSplitterSourceModel
    {
        public string Values { get; set; } = "1,2,3";
    }

    public class TupleSplitterTargetModel
    {
        public string Value1 { get; set; }

        public string Value2 { get; set; }

        public string Value3 { get; set; }
    }

    public class TupleSplitterMapper :
        Mapper<TupleSplitterSourceModel, TupleSplitterTargetModel>
    {
        public TupleSplitterMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);

            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2, t => t.Value3)
                .Using(StringTupleSplitter);
        }

        private (string, string, string) StringTupleSplitter(
            string values)
        {
            var splitted = values.Split(',');

            return (splitted[0], splitted[1], splitted[2]);
        }
    }
}