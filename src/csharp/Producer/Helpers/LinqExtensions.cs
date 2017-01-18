namespace Producer.Helpers
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
}