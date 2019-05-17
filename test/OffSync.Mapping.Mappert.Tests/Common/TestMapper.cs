using System;
using System.Collections.Generic;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    public class TestMapper :
        Mapper<SourceModel, TargetModel>
    {
        public int MyProperty { get; set; }

        public TestMapper()
        {
            Map(s => s.Name)
                .To(t => t.Description);

            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2)
                .Using(ValueSplitter);

            IgnoreSource(s => s.Ignored);

            IgnoreTarget(t => t.Excluded);
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
