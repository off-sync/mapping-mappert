using System;

namespace OffSync.Mapping.Mappert.DynamicMethods.Tests
{
    public class Reference
    {
        public void Map(
            Source source,
            Target target,
            Delegate builder)
        {
            var froms = new object[1];

            froms[0] = source.Id;

            var value = ((int, string))builder.DynamicInvoke(froms);

            target.Id = value.Item1;

            target.Value1 = value.Item2;
        }
    }

    public class Source
    {
        public int Id { get; set; }

        public string Values { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }
}
