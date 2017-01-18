namespace Producer
{
    using System;
    using System.Collections.Generic;

    public static class LinqExtensions
    {
        public static void Each<T>(this IEnumerable<T> list, Action<T> handler)
        {
            if (list == null)
                return;

            foreach (var t in list)
                handler(t);
        }
    }

    public static class Guard
    {
        public static void AgainstNullOrEmptyString(
            string value,
            string propertyName)
        {
            if (value == null)
                throw new ArgumentNullException(propertyName);

            if (Equals(value, string.Empty))
                throw new ArgumentException(propertyName);
        }
    }
}