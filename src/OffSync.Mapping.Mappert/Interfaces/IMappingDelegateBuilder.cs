using System;
using System.Reflection;

namespace OffSync.Mapping.Mappert.Interfaces
{
    public interface IMappingDelegateBuilder
    {
        /// <summary>
        /// Must return a delegate that when executed, sets the target
        /// properties from the source properties.
        /// 
        /// If the builder is provided, this must be used to build the
        /// target values from the source properties.
        /// 
        /// If the builder is null, exactly one source and target 
        /// property must be provided. The target property must bes set
        /// directly from the source property.
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="sourceProperties"></param>
        /// <param name="targetType"></param>
        /// <param name="targetProperties"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        Delegate CreateMappingDelegate(
            Type sourceType,
            PropertyInfo[] sourceProperties,
            Type targetType,
            PropertyInfo[] targetProperties,
            Delegate builder);
    }
}
