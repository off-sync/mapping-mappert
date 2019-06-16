/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    public interface ILookupService
    {
        int Lookup(string value);
    }

    public class ParsingLookupService :
        ILookupService
    {
        public int Lookup(string value) => int.Parse(value);
    }

    public class TestMapper :
        Mapper<SourceModel, TargetModel>
    {
        private readonly ILookupService _lookupService = new ParsingLookupService();

        public TestMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);

            Map(s => s.Name)
                .To(t => t.Description);

            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2, t => t.Value3)
                .Using(ValueTupleSplitter);

            Map(s => s.Values)
                .To(t => t.MoreValue1, t => t.MoreValue2, t => t.MoreValue3)
                .Using(ObjectArraySplitter);

            IgnoreSource(s => s.Ignored);

            IgnoreTarget(t => t.Excluded);

            Map(s => s.LookupValue)
                .To(t => t.LookupId)
                .Using(_lookupService.Lookup);

            MapItems(s => s.ItemsEnumerable)
                .To(t => t.ItemsArray);

            MapItems(s => s.ItemsArray)
                .To(t => t.ItemsCollection);

            MapItems(s => s.ItemsArray)
                .To(t => t.ItemsList);

            MapItems(s => s.Numbers)
                .To(t => t.NumbersCollection);

            MapItems(s => s.Numbers)
                .To(t => t.NumbersList);

            Map(s => s.Nested)
                .To(t => t.NestedToo);

            Map(s => s.Id, s => s.Name)
                .To(t => t.IdAndName)
                .Using(IdAndNameBuilder);

            IgnoreTarget(t => t.Generated);

            Map(s => s.CreatedOn)
                .To(t => t.CreatedOn)
                .Using(dto => dto.UtcDateTime);
        }

        protected override TargetModel CreateTarget()
        {
            return new TargetModel()
            {
                Generated = "generated",
            };
        }

        private string IdAndNameBuilder(
            int id,
            string name)
        {
            return $"#{id} ({name})";
        }

        public TestMapper(
            Action<IMapperBuilder<SourceModel, TargetModel>> withRules) :
            base(withRules)
        {
        }

        private (string, string, string) ValueTupleSplitter(
            string values)
        {
            var splittedValues = values.Split(',');

            return (splittedValues[0], splittedValues[1], splittedValues[2]);
        }

        private object[] ObjectArraySplitter(
            string values)
        {
            return values.Split(',');
        }
    }
}
