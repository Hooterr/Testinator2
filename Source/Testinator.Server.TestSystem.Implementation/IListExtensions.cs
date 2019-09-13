using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class IListExtensions
    {
        public static int RemoveAllLast<TSource>(this IList<TSource> source, Predicate<TSource> predicate)
        {
            var numberOfDeletedElements = 0;
            for (var i = source.Count - 1; i >= 0; i--)
            {
                if (predicate(source[i]))
                {
                    source.RemoveAt(i);
                    numberOfDeletedElements++;
                }
                else
                    break;
            }
            return numberOfDeletedElements;
        }
    }
}
