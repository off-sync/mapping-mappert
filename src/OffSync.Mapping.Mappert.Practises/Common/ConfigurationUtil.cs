using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            var type = GetPreferredMappingDelegateBuilder();

            if (type == null)
            {
                // try to load assemblies that might not be referenced directly
                LoadAllAssemblies();

                type = GetPreferredMappingDelegateBuilder();
            }

            if (type == null)
            {
                throw new InvalidOperationException("no mapping delegate registered");
            }

            return (IMappingDelegateBuilder)Activator.CreateInstance(type);
        }

        private static Type GetPreferredMappingDelegateBuilder()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetCustomAttributes(
                    typeof(RegisterMappingDelegateBuilderAttribute),
                    true))
                .Cast<RegisterMappingDelegateBuilderAttribute>()
                .OrderByDescending(a => a.Preference)
                .FirstOrDefault()?
                .BuilderType;
        }

        private static void LoadAllAssemblies()
        {
            var loadedAssemblies = new HashSet<string>(
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .Select(a => a.Location));

            var assembliesToLoad = Directory
                .GetFiles(
                    AppDomain
                        .CurrentDomain
                        .BaseDirectory,
                    "*.dll")
                .Where(p => !loadedAssemblies.Contains(p));

            foreach (var path in assembliesToLoad)
            {
                // load into execution context
                AppDomain
                    .CurrentDomain
                    .Load(AssemblyName.GetAssemblyName(path));
            }
        }
    }
}
