namespace OffSync.Mapping.Mappert.Practises
{
    public delegate void MappingDelegate<in TSource, in TTarget>(
        TSource source,
        TTarget target);
}
