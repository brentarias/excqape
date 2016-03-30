using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excqape
{
    public interface ICommand<TCommand>
    {
        Task Handle(TCommand command);
    }
}
