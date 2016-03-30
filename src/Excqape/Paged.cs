using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excqape
{
    /// <summary>
    /// Paging metadata and data container.
    /// </summary>
    /// <remarks>
    /// The code sample demonstrates a synchronous model for paging.  An async model
    /// is preferred, but can only be implemented on a per-technology basis.
    /// </remarks>
    /// <code>
    /// public static Paged<T> ApplyPaging(IQueryable<T> query, PagingInformation paging)
    /// {
    ///    int count = query.Count();
    ///    var page = query.Skip(paging.PageSize * paging.PageIndex).Take(paging.PageSize).ToList();
    ///    return new Paged<T>(
    ///        paging: paging,
    ///        pageCount: (count + (paging.PageSize - 1)) / paging.PageSize,
    ///        itemCount: count,
    ///        page: page.AsReadOnly());
    /// }
    /// </code>
    /// <typeparam name="T"></typeparam>
    public sealed class Paged<T>
    {
        public readonly PagingInformation Paging;
        public readonly int PageCount;
        public readonly int ItemCount;
        public readonly ReadOnlyCollection<T> Page;

        public Paged(PagingInformation paging, int pageCount, int itemCount, ReadOnlyCollection<T> page)
        {
            this.Paging = paging;
            this.PageCount = pageCount;
            this.ItemCount = itemCount;
            this.Page = page;
        }
    }
}
