
using System;

using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class SimpleSourceModel
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "2";

        public DateTimeOffset CreatedOn { get; set; } = new DateTimeOffset(2003, 4, 5, 6, 7, 8, TimeSpan.FromHours(9));

        public SimpleSourceNested Nested { get; set; } = new SimpleSourceNested() { Key = 10, Value = "11" };

        public int? Nullable { get; set; } = 12;
    }

    public class SimpleSourceNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }

    public class SimpleTargetModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public SimpleTargetNested Nested { get; set; }

        public int? Nullable { get; set; }
    }

    public class SimpleTargetNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }

    public class SimpleMapper :
        Mapper<SimpleSourceModel, SimpleTargetModel>
    {
        public SimpleMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);

            Map(s => s.Name)
                .To(t => t.Description);

            Map(s => s.CreatedOn)
                .To(t => t.CreatedOn)
                .Using(dto => dto.UtcDateTime);
        }
    }
}