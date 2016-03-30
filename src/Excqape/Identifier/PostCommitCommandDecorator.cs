using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excqape.Identifier
{
    /// <summary>
    /// Decorates a command handler that coordinates several transactive command handlers.
    /// </summary>
    /// <code>
    /// using SimpleInjector;
    /// using SimpleInjector.Extensions;
    /// 
    /// container.RegisterManyForOpenGeneric(
    ///    typeof(ICommandHandler<>), 
    ///    typeof(ICommandHandler<>).Assembly);
    ///
    /// container.RegisterDecorator(
    ///    typeof(ICommandHandler<>), 
    ///    typeof(TransactionCommandHandlerDecorator<>));
    ///
    /// container.RegisterDecorator(
    ///    typeof(ICommandHandler<>), 
    ///    typeof(PostCommitCommandHandlerDecorator<>));
    ///
    /// container.RegisterPerWebRequest<PostCommitRegistratorImpl>();
    /// container.Register<IPostCommitRegistrator>(
    ///    () => container.GetInstance<PostCommitRegistratorImpl>());
    /// </code>
    /// <typeparam name="T"></typeparam>
    public sealed class PostCommitCommandDecorator<T> : ICommand<T>
    {
        private readonly ICommand<T> decorated;
        private readonly PostCommitRegistratorImpl registrator;

        public PostCommitCommandDecorator(
            ICommand<T> decorated, PostCommitRegistratorImpl registrator)
        {
            this.decorated = decorated;
            this.registrator = registrator;
        }

        public async Task Handle(T command)
        {
            try
            {
                await this.decorated.Handle(command);

                this.registrator.ExecuteActions();
            }
            finally
            {
                this.registrator.Reset();
            }
        }

        //to support .net 4, consider using the code from here:
        //http://blogs.msdn.com/b/pfxteam/archive/2010/11/21/10094564.aspx
        //Specifically this...
        //public static Task<T2> Then<T1, T2>(this Task<T1> first, Func<T1, Task<T2>> next)
        //{
        //    if (first == null) throw new ArgumentNullException("first");
        //    if (next == null) throw new ArgumentNullException("next");

        //    var tcs = new TaskCompletionSource<T2>();
        //    first.ContinueWith(delegate
        //    {
        //        if (first.IsFaulted) tcs.TrySetException(first.Exception.InnerExceptions);
        //        else if (first.IsCanceled) tcs.TrySetCanceled();
        //        else
        //        {
        //            try
        //            {
        //                var t = next(first.Result);
        //                if (t == null) tcs.TrySetCanceled();
        //                else
        //                    t.ContinueWith(delegate
        //                    {
        //                        if (t.IsFaulted) tcs.TrySetException(t.Exception.InnerExceptions);
        //                        else if (t.IsCanceled) tcs.TrySetCanceled();
        //                        else tcs.TrySetResult(t.Result);
        //                    }, TaskContinuationOptions.ExecuteSynchronously);
        //            }
        //            catch (Exception exc) { tcs.TrySetException(exc); }
        //        }
        //    }, TaskContinuationOptions.ExecuteSynchronously);
        //    return tcs.Task;
        //}
    }
}
