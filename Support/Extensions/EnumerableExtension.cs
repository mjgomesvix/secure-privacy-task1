using System.Collections.Generic;
using System.Linq;

namespace Support.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable?.Any() != true;

        public static IEnumerable<T> Pagination<T>(this IEnumerable<T> enumerable, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            return enumerable.AsQueryable().Skip(skip).Take(pageSize);
        }
    }
}
