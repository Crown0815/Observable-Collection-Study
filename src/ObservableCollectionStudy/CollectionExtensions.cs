using System.Collections;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;

namespace ObservableCollectionStudy
{
    internal static class CollectionExtensions
    {
        public static GenericCollectionAssertions<T> Should<T>(this IEnumerable collection)
        {
            return collection.Cast<T>().Should();
        }
    }
}