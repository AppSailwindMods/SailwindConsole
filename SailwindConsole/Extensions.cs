using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole
{
    public static class Extensions
    {
        public static IEnumerable<E> Map<T, E>(this IEnumerable<T> source, Func<T, E> action)
        {
            return source.Map((item, _) => action(item));
        }

        public static IEnumerable<E> Map<T, E>(this IEnumerable<T> source, Func<T, int, E> action)
        {
            int index = 0;

            foreach (T item in source)
            {
                E value = action(item, index);
                index++;
                if (value != null)
                    yield return value;
            }
        }
    }
}
