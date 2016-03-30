using System;
using System.Linq.Expressions;

namespace Excqape.Sortation
{
    public static class Sortation
    {
        public static OrderedSortation<T> OrderBy<T, TKey>(this T data, Expression<Func<T, TKey>> sort) where T : IQuerySpec
        {
            var propInfo = OrderedSortation<T>.VerifyParams(sort);
            var sortParams = new OrderedSortation<T>();
            sortParams.Add(propInfo.Name, SortDirection.ASC);

            return sortParams;
        }
    }
}
