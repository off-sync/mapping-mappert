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
            if (type.IsArray)
            {
                itemsType = type.GetElementType();

                return true;
            }

            if (IsGenericEnumerable(type) ||
                type.GetInterfaces().Any(IsGenericEnumerable))
            {
                itemsType = type.GetGenericArguments()[0];

                return true;
            }

            itemsType = null;

            return false;
        }

        private static bool IsGenericEnumerable(
            Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static bool TryGetTargetItemsType(
            this Type type,
            out Type itemsType)
        {
            if (type.IsArray)
            {
                itemsType = type.GetElementType();

                return true;
            }

            if (IsGenericCollection(type) ||
                type.GetInterfaces().Any(IsGenericCollection))
            {
                itemsType = type.GetGenericArguments()[0];

                return true;
            }

            itemsType = null;

            return false;
        }

        private static bool IsGenericCollection(
            Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(ICollection<>);
        }
    }
}
