using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Editors
{
    public static class ListExtensions
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
