namespace OffSync.Mapping.Mappert
{
    public class Mapper<TSource, TTarget> :
        AbstractMapper<TSource, TTarget>
        where TTarget : new()
    {
        override protected TTarget CreateTarget()
        {
            return new TTarget();
        }
    }
}
