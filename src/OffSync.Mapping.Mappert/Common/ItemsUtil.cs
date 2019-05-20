using System;
using System.Collections.Generic;
using System.Linq;

namespace OffSync.Mapping.Mappert.Common
{
    public static class ItemsUtil
    {
        public static bool TryGetSourceItemsType(
            this Type type,
            out Type itemsType)
        {
            itemsType = GetSourceItemsTypeOrDefault(type);

            return itemsType != null;
        }

        public static Type GetSourceItemsTypeOrDefault(
            this Type type)
        {
            if (type.IsArray) // T[] is supported
            {
                return type.GetElementType();
            }

            if (type.IsInterface &&
                IsGenericEnumerable(type)) // IEnumerable<T> is supported
            {
                return type.GetGenericArguments()[0];
            }

            if (type.IsInterface ||
                (type.IsClass &&
                !type.IsAbstract))
            {
                var iface = type
                    .GetInterfaces()
                    .FirstOrDefault(IsGenericEnumerable);

                if (iface != null) // IEnumerable<T> is supported
                {
                    return iface.GetGenericArguments()[0];
                }
            }

            return null;
        }

        public static bool TryGetTargetItemsType(
            this Type type,
            out Type itemsType)
        {
            itemsType = GetTargetItemsTypeOrDefault(type);

            return itemsType != null;
        }

        public static Type GetTargetItemsTypeOrDefault(
            this Type type)
        {
            if (type.IsArray) // T[] is supported
            {
                return type.GetElementType();
            }

            if (type.IsInterface &&
                (IsGenericEnumerable(type) || // IEnumerable<T> is supported
                    IsGenericCollection(type) || // I{ReadOnly}Collection<T> is supported
                    IsGenericList(type))) // I{ReadOnly}List<T> is supported
            {
                return type.GetGenericArguments()[0];
            }

            if (type.IsClass &&
                !type.IsAbstract)
            {
                var iface = type
                    .GetInterfaces()
                    .FirstOrDefault(IsGenericCollection);

                if (iface != null)
                {
                    // concrete classes must implement ICollection<T>
                    return iface.GetGenericArguments()[0];
                }
            }

            return null;
        }

        #region Helpers
        private static bool IsGenericEnumerable(
            Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        private static bool IsGenericCollection(
            Type type)
        {
            return type.IsGenericType &&
                (type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>));
        }

        private static bool IsGenericList(
            Type type)
        {
            return type.IsGenericType &&
                (type.GetGenericTypeDefinition() == typeof(IList<>) ||
                type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>));
        }
        #endregion
    }
}
