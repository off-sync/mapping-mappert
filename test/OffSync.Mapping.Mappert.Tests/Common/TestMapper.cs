using System;
using System.Collections.Generic;

using OffSync.Mapping.Mappert.DynamicMethods;
using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;

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

        public TestMapper()
        {
            WithMappingDelegateBuilder(new DynamicMethodMappingDelegateBuilder());

            Map(s => s.Name)
                .To(t => t.Description);

            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2)
                .Using(ValueSplitter);

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
        }

        public TestMapper(
            Action<IMapperBuilder<SourceModel, TargetModel>> withRules) :
            base(withRules)
        {
        }

        public IEnumerable<MappingRule> CheckedMappingRules => GetCheckedMappingRules();

        private (string, string) ValueSplitter(
            string values)
        {
            var splittedValues = values.Split(',');

            return (splittedValues[0], splittedValues[1]);
        }
    }
}
