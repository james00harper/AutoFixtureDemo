using IDL.Service.Commands.Parameters;
using IDL.Service.Models;

namespace IDL.Service.Commands.Interfaces
{
    public interface ICreateUserCommand
    {
        VouchercloudUser Execute(CreateUserParameters parameters);
    }
}
