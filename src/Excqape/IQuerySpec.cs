using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excqape
{
    public interface IQuerySpec { }

    public interface IQuerySpec<TResult> : IQuerySpec { }

    public interface IPagedQuerySpec<TResult> : IQuerySpec<Paged<TResult>>
    {
        PagingInformation Paging { get; set; }
    }
}
