using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDL.Service.Commands.Parameters;

namespace IDL.Service.Commands.Interfaces
{
    public interface IDeleteUserCommand
    {
        void Execute(DeleteUserParameters parameters);
    }
}
