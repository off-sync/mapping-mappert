using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    public static class TestUtil
    {
        public static IEnumerable<T> Yield<T>(
            this T item)
        {
            yield return item;
        }
    }
}
