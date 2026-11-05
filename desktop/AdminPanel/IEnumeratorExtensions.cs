using System.Collections;

namespace AdminPanel
{
    internal static class IEnumeratorExtensions
    {
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable ToIEnumerable(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}
