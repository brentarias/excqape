using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excqape
{
    public sealed class PagingInformation
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        /// <summary>
        /// Paging parameters for IQuerySpec&lt;T&gt; objects.
        /// </summary>
        /// <param name="pageIndex">Zero-indexed page to return</param>
        /// <param name="pageSize">Number of entries per page</param>
        public PagingInformation(int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException("pageIndex");
            if (pageSize <= 0) throw new ArgumentOutOfRangeException("pageSize");

            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }
    }
}
