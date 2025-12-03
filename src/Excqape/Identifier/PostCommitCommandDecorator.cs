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
    }
}
