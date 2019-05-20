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
