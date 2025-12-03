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
    /// Ideally, command objects and their handlers would NOT be required to return
    /// database generated Ids for newly created records/entities. However, usually the only 
    /// way this can be avoided is by having application code generate a GUID that is supplied 
    /// as part of the command - which becomes the primary key in the DB. Yet this too is not ideal,
    /// as database indexing on GUIDs is inefficient compared to integer based keys. Accordingly,
    /// this interface exists to provide a way for command handlers to return (non-GUID) database 
    /// generated Ids, without inviting the problem associated with GUIDs as primary keys.
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
