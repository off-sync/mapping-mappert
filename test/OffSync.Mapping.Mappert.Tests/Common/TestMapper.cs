namespace OffSync.Mapping.Mappert.Tests.Common
{
    public class TestMapper :
        Mapper<SourceModel, TargetModel>
    {
        public TestMapper()
        {
            Map(s => s.Name)
                .To(t => t.Description);
        }
    }
}
