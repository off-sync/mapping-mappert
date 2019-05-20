using System;
using System.Collections.Generic;
using System.Linq;

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
                return ((IEnumerable<T>)value).Count();
            }

            throw new ArgumentException(
                $"value has unsupported items type: '{type.FullName}'",
                nameof(value));
        }

        public static TTargetItems[] FillArray<TTargetItems>(
            TTargetItems[] array,
            IEnumerable<TTargetItems> items)
        {
            var i = 0;

            foreach (var item in items)
            {
                array[i++] = item;
            }

            return array;
        }

        public static TTargetItems[] FillArrayWithBuilder<TSourceItems, TTargetItems>(
            TTargetItems[] array,
            IEnumerable<TSourceItems> items,
            Func<TSourceItems, TTargetItems> builder)
        {
            var i = 0;

            foreach (var item in items)
            {
                array[i++] = builder(item);
            }

            return array;
        }

        public static T FillCollection<T, TItem>(
            T collection,
            IEnumerable<TItem> items)
            where T : class, ICollection<TItem>
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static TTarget FillCollectionWithBuilder<TTarget, TSourceItems, TTargetItems>(
            TTarget collection,
            IEnumerable<TSourceItems> items,
            Func<TSourceItems, TTargetItems> builder)
            where TTarget : class, ICollection<TTargetItems>
        {
            foreach (var item in items)
            {
                collection.Add(
                    builder(item));
            }

            return collection;
        }

        public static List<TItem> FillList<TItem>(
            List<TItem> list,
            IEnumerable<TItem> items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }

            return list;
        }

        public static List<TTargetItems> FillListWithBuilder<TSourceItems, TTargetItems>(
            List<TTargetItems> list,
            IEnumerable<TSourceItems> items,
            Func<TSourceItems, TTargetItems> builder)
        {
            foreach (var item in items)
            {
                list.Add(
                    builder(item));
            }

            return list;
        }
    }
}
