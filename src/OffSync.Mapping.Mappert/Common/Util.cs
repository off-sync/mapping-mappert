using System;

namespace OffSync.Mapping.Mappert.Common
{
    public static class Util
    {
        public static bool Try(
            this Action action,
            out Exception exception)
        {
            try
            {
                action();

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                exception = ex;

                return false;
            }
        }

        public static bool Try<T>(
            this Func<T> function,
            out T result,
            out Exception exception)
        {
            try
            {
                result = function();

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                result = default;

                exception = ex;

                return false;
            }
        }
    }
}
