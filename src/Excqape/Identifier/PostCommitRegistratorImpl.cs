using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excqape.Identifier
{
    /// <summary>
    /// Allows a transaction-supporting command handler to enlist post-commit assignment of DB-generated Identifiers.
    /// </summary>
    public sealed class PostCommitRegistratorImpl : IPostCommitRegistrar
    {
        public event Action Committed = () => { };

        public void ExecuteActions()
        {
            this.Committed();
        }

        public void Reset()
        {
            // Clears the list of actions.
            this.Committed = () => { };
        }
    }
}
