using System;
using System.Linq;

using OffSync.Mapping.Practises;

namespace OffSync.Mapping.Mappert.Common
{
    public static class MappersUtil
    {
        public static bool TryCreateAutoMapper(
            Type sourceType,
            Type targetType,
            out object mapper,
            out Exception exception)
        {
            try
            {
                mapper = CreateAutoMapper(
                    sourceType,
                    targetType);

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                mapper = null;

                exception = ex;

                return false;
            }
        }

        public static object CreateAutoMapper(
            Type sourceType,
            Type targetType)
        {
            #region Pre-conditions
            if (sourceType == null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            if (targetType.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException(
                    $"cannot create auto-mapper: target type '{targetType.Name}' does not have a parameterless constructor",
                    nameof(targetType));
            }
            #endregion

            var mapperType = typeof(Mapper<,>)
                .MakeGenericType(
                    sourceType,
                    targetType);

            return Activator.CreateInstance(mapperType);
        }

        public static Delegate CreateMapperDelegate(
            object mapper)
        {
            #region Pre-conditions
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            var mapperTypes = mapper
                .GetType()
                .GetInterfaces()
                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapper<,>));

            if (!mapperTypes.Any())
            {
                throw new ArgumentException(
                    $"invalid type: must implement IMapper<,>",
                    nameof(mapper));
            }

            if (mapperTypes.Skip(1).Any())
            {
                throw new ArgumentException(
                    $"invalid type: must implement IMapper<,> exactly once",
                    nameof(mapper));
            }

            var mapperType = mapperTypes.Single();

            var sourceType = mapperType.GetGenericArguments()[0];

            var targetType = mapperType.GetGenericArguments()[1];
            #endregion

            var mapMethod = mapperType
                .GetMethod(
                    "Map",
                    new Type[] { sourceType });

            var builderType = typeof(Func<,>)
                .MakeGenericType(
                    sourceType,
                    targetType);

            return Delegate.CreateDelegate(
                builderType,
                mapper,
                mapMethod);
        }
    }
}
