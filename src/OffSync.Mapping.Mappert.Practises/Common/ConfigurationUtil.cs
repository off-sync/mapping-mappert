using System;
using System.Linq;

using OffSync.Mapping.Mappert.Practises.Configuration;

namespace OffSync.Mapping.Mappert.Practises.Common
{
    public static class ConfigurationUtil
    {
        public static void EnsureValidMappingDelegateBuilderType(
            Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!typeof(IMappingDelegateBuilder).IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    $"invalid type '{type.FullName}': must be assignable to {nameof(IMappingDelegateBuilder)}");
            }
        }

        public static IMappingDelegateBuilder GetRegisteredMappingDelegateBuilder()
        {
            var type = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetCustomAttributes(
                    typeof(RegisterMappingDelegateBuilderAttribute),
                    true))
                .Cast<RegisterMappingDelegateBuilderAttribute>()
                .OrderByDescending(a => a.Preference)
                .FirstOrDefault()?
                .BuilderType;

            if (type == null)
            {
                throw new InvalidOperationException("no mapping delegate registered");
            }

            return (IMappingDelegateBuilder)Activator.CreateInstance(type);
        }
    }
}
