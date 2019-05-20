using System;
using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.DynamicMethods.Common
{
    public static class ItemsUtil
    {
        public static int GetItemsCount<T>(
            object value)
        {
            var type = value.GetType();

            if (type.IsArray)
            {
                return ((Array)value).Length;
            }

            if (typeof(ICollection<T>).IsAssignableFrom(type))
            {
                return ((ICollection<T>)value).Count;
            }

            if (typeof(IEnumerable<T>).IsAssignableFrom(type))
            {
                var count = 0;

                foreach (var item in (IEnumerable<T>)value)
                {
                    count++;
                }

                return count;
            }

            throw new ArgumentException(
                $"value has unsupported items type: '{type.FullName}'",
                nameof(value));
        }

        public static T[] FillArray<T>(
            T[] array,
            IEnumerable<T> items)
        {
            var i = 0;

            foreach (var item in items)
            {
                array[i++] = item;
            }

            return array;
        }

        public static TTarget[] FillArrayWithBuilder<TSource, TTarget>(
            TTarget[] array,
            IEnumerable<TSource> items,
            Func<TSource, TTarget> builder)
        {
            var i = 0;

            foreach (var item in items)
            {
                array[i++] = builder(item);
            }

            return array;
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

        public static ICollection<T> FillCollection<T>(
            ICollection<T> collection,
            IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static ICollection<TTarget> FillCollectionWithBuilder<TSource, TTarget>(
            ICollection<TTarget> collection,
            IEnumerable<TSource> items,
            Func<TSource, TTarget> builder)
        {
            foreach (var item in items)
            {
                collection.Add(
                    builder(item));
            }

            return collection;
        }
    }
}
