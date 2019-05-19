using System;

namespace OffSync.Mapping.Mappert.DynamicMethods.Tests
{
    public class ReferenceMapper
    {
        public void Map(
            Source source,
            Target target)
        {
            Func<string, (int, string)> builder = TupleSplitter;

            object[] froms = new object[] { source.Id };

            var value = ((int, string))builder.DynamicInvoke(froms);

            target.Id = value.Item1;

            target.Value1 = value.Item2;
        }

        private (int, string) TupleSplitter(
            string values)
        {
            var splitted = values.Split(',');

            return (int.Parse(splitted[0]), splitted[1]);
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
