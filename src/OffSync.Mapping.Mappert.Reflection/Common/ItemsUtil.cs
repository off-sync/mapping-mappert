using System;
using System.Collections;
using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.Reflection.Common
{
    public static class ItemsUtil
    {
        public static int GetItemsCount(
            object value)
        {
            var type = value.GetType();

            if (type.IsArray)
            {
                return ((Array)value).Length;
            }

            if (typeof(ICollection).IsAssignableFrom(type))
            {
                return ((ICollection)value).Count;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var count = 0;

                foreach (var item in (IEnumerable)value)
                {
                    count++;
                }

                return count;
            }

            throw new ArgumentException(
                $"value has unsupported items type: '{type.FullName}'",
                nameof(value));
        }

        public static Array CreateArray(
            Type type,
            int length)
        {
            return (Array)Activator.CreateInstance(
                type.MakeArrayType(),
                new object[] { length });
        }

        public static object CreateCollection(
            Type type,
            Type itemsType)
        {
            if (type.IsClass)
            {
                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(
                typeof(List<>).MakeGenericType(itemsType));
        }
    }
}
