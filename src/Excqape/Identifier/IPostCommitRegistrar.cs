using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excqape.Identifier
{
    /// <summary>
    /// Allows transaction management code to work with ICommandSpec&lt;T&gt; />
    /// </summary>
    public interface IPostCommitRegistrar
    {
        event Action Committed;
    }
}
