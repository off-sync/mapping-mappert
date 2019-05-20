namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public delegate TTarget BuilderDelegate<in TSource, out TTarget>(
        TSource source);
}
