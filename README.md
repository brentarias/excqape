# The Excqape Project

Excqape provides Data Access Layer (DAL) and CQRS interfaces. 
This library replaces "repositories" with a much more effective
abstraction.  The interfaces form a wrapper for any storage 
paradigm (relational, key-value, document, etc.), while still 
leveraging the unique transactive or query power of each.

This library supports .NET 4.6 and .NET Core.
  
These interfaces support the convergence of several design patterns: 
the [Command Pattern](https://en.wikipedia.org/wiki/Command_pattern), 
the [Decorator Pattern](https://en.wikipedia.org/wiki/Decorator_pattern), 
and the 'Exchange' Pattern.

When these patterns are combined, it is called the "Escape" pattern.

## Sections

 * [The Exchange Pattern](exchange)
 * [The Escape Pattern](escape)
 * [Credits, Documentation & Background](documentation)

## <a name="exchange">The Exchange Pattern</a>

The Exchange Pattern combines generics and liskov substitution to 
allow *the base-type of a function's input parameter* to dictate the 
function's *return type*.  

To provide an example, first a couple definitions are needed:

    public interface IQuerySpec<TResult> {}

    public class FindUsersBySearchTextQuery : IQuerySpec<User[]>
    {
        public string SearchText { get; set; }
        public bool IncludeInactiveUsers { get; set; }
    }
    
In abstract terms, the above code is simultaneously declaring the 
parameters *and the projection* "shape" of a single query.  Thus
the *return type* from the query is generated automatically in the 
following example:

    public class QueryEngine { 
      public TResult Execute<TResult>(IQuerySpec<TResult> query){
          //...
      }  
    }

Said differently, the proper return type is given "in exchange" for 
the supplied [actual parameter](https://en.wiktionary.org/wiki/actual_parameter). 

>The "Excqape" library name contains several allusions.  The "Exc" hints 
at the Exchange pattern.  The "cq" hints at commands and queries.   

The Exchange pattern has been applied by many developers in 
many contexts for at least a decade, but to my knowledge it 
had never been given a name.  It's about time.  If anyone is
aware of a prior-given name, create a github issue so that I
can investigate.

In the Excqape library, the Exchange pattern is more subtle than what
is shown in the above `QueryEngine` illustration.  A concrete class that 
performs a query must implement this interface: 

    public interface IQuery<TQuery, TResult> where TQuery : IQuerySpec<TResult>
    {
        TResult Handle(TQuery query);
    }    

For example:

    public class FindUsersBySearchTextQueryHandler
        : IQuery<FindUsersBySearchTextQuery, User[]>
    {
        private readonly NorthwindUnitOfWork db;
    
        public FindUsersBySearchTextQueryHandler(NorthwindUnitOfWork db)
        {
            this.db = db;
        }
    
        public User[] Handle(FindUsersBySearchTextQuery query)
        {
            return (
                from user in this.db.Users
                where user.Name.Contains(query.SearchText)
                select user)
                .ToArray();
        }
    }

The above approach allows query implementations to be swapped, depending
on the type or paradigm of the storage being accessed.  The example concrete 
implementation shown above assumes a database object that supports `IQueryable`.
It should be readily apparent how to create a different implementation that
targets a database which has no IQueryable support at all.  Thus Excqape is
independent of the storage tool or paradigm chosen.

The Exchange pattern should not be confused with the [Specification pattern](https://en.wikipedia.org/wiki/Specification_pattern); 
The Excqape library does not offer Specification pattern interfaces.

## <a name="escape">The Escape Pattern</a>

Today's software typically demands polyglot persistence, or outright migration from 
one storage paradigm to another (e.g. from relational to key-value).  This is where 
a Data Abstraction Layer (DAL) is pertinent, except that nobody seems to have ever 
figured out how to make a successful DAL.

The crux of the problem is that software engineers try to solve the DAL problem with 
code-generators or application frameworks, instead of recognizing that the only real 
answer is to apply a pattern, rather than middleware.

This library is designed to support a pattern - the Escape pattern. The library is 
nothing but a set of interfaces.  It is your job to follow the pattern and provide the 
implementation - as described by the links in the [Credits, Documentation & Background](documentation)
section.

The articles which describe the "Escape" pattern propose that it can be used for much 
more than a DAL mechanism.  For example, Microsoft invented OData as a quasi "LINQ to 
web services" mechanism.  The only way an OData-based web service can work well, is 
if the web service's internal implementation has a full-blown LINQ provider 
(IQueryable implementation).  As is, the only "full-blown" implementations that 
can be found are for NHibernate and Entity Framework.  Industry-wide, few projects 
use those back-end ORM's, and thus it is hard to find a market for OData. 

The "Escape" pattern is a great way to solve the problem OData was invented for, but 
without being tied to any platform or persistence technology.  It is true that the 
Escape pattern can be used for much more than a DAL, but for the uninitiated it is
perhaps best at first to apply it only for DAL needs, and then expand from there as 
circumstances warrant.

## <a name="documentation">Credits, Documentation & Background</a>

This code was taken directly from Steven's .NET Junkie blog entries:

 * [Meanwhile On The Command Side of My Architecture](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=91)
 * [Meanwhile On The Query Side of My Architecture](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=92)

Steven's articles implicitly provide copious documentation of how to use this 
library.  Be sure also to [read how paging works with queries](https://solidservices.codeplex.com/discussions/574373).

My initial checkin uses slightly different names for the interfaces, and
the signatures have all been made asynchronous (`Task` based).  Otherwise,
it is Steven's code.

The use and combination of the patterns (Command, Decorator, and 
Exchange) was a technique I had already adopted due to (1) cross-paradigm 
limitations of IQueryable and (2) my disappointmment with typical ORM, DAL 
and Repository tools and patterns.

While building my own interfaces to support these patterns, I discovered 
Steven's blog entries. Steven's code was exactly what I wanted, and already 
had painful details worked out (e.g. handling row identity responses for 
Commands and handling paging).  Hopefully others will benefit by finding 
his code in this convenient repo!