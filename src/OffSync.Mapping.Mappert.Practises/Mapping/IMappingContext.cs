namespace OffSync.Mapping.Mappert.Practises.Mapping
{
    public interface IMappingContext
    {
        bool TryGetMapping(
            object source,
            out object target);

        void AddMapping(
            object source,
            object target);
    }
}
