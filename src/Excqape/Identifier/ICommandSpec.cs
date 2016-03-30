using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excqape.Identifier
{
    /// <summary>
    /// Defines a property through which database generated Ids can be accessed in command-spec objects.
    /// </summary>
    /// <remarks>
    /// Wherever possible, application design should avoid using this interface.  Said again,
    /// when possible, command objects and handlers should NOT be required to return
    /// database generated Ids.  A design based on CQRS is a primary way to achieve this goal.
    /// However, too many teams and software designs might be unable or unready to comply with
    /// this strategy.  This interface exists to provide support in such cases.
    /// Using this interface still allows asynchrony via async/await, but negates asynchrony
    /// based on a queuing or service bus strategy.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class ICommandSpec<T>
    {
        /// <summary>
        /// A newly created DB identifier is assigned and accessed through this property.
        /// </summary>
        T Identifier { get; set; }
    }
}
