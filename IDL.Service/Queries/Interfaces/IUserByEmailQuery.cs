using IDL.Service.Models;
using IDL.Service.Queries.Parameters;

namespace IDL.Service.Queries.Interfaces
{
    public interface IUserByEmailQuery
    {
        VouchercloudUser Execute(UserByEmailParameters queryParameters);
    }
}
