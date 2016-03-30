using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excqape
{
    /// <summary>
    /// A factory for query handlers.
    /// </summary>
    /// <remarks>
    /// The code below is a sample implementation that depends on SimpleInjector.
    /// </remarks>
    /// <code>
    /// public class QueryProcessor : IQueryProcessor
    /// {
    ///     private readonly Container container;
    /// 
    ///     public QueryProcessor(Container container)
    ///     {
    ///         this.container = container;
    ///     }
    /// 
    ///     [DebuggerStepThrough]
    ///     public TResult Process<TResult>(IQuery<TResult> query)
    ///     {
    ///         var handlerType =
    ///             typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
    /// 
    ///         dynamic handler = container.GetInstance(handlerType);
    /// 
    ///         return handler.Handle((dynamic)query);
    ///     }
    /// }
    /// </code>
    public interface IQueryProcessor
    {
        Task<TResult> Execute<TResult>(IQuerySpec<TResult> query);
    }
}
