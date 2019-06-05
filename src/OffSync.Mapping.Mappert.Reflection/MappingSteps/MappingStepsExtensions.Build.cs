namespace OffSync.Mapping.Mappert.Reflection.MappingSteps
{
    public static partial class MappingStepsExtensions
    {
        public static object Build(
            this MappingStep mappingStep,
            object[] froms)
        {
            return mappingStep
                .BuilderInvoke
                .Invoke(
                    mappingStep.Builder,
                    froms);
        }
    }
}
