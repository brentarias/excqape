using System;
using System.Linq.Expressions;

namespace Excqape.Sortation
{
    public class Sortation<T>
    {
        public static OrderedSortation<T> OrderBy<TKey>(Expression<Func<T, TKey>> sort) where TKey : IQuerySpec
        {
            var propInfo = OrderedSortation<T>.VerifyParams(sort);
            var sortParams = new OrderedSortation<T>();
            sortParams.Add(propInfo.Name, SortDirection.ASC);

            return sortParams;
        }
    }
}
