using System.Net;

using IDL.Service.Commands.Interfaces;
using IDL.Service.Commands.Parameters;
using IDL.Service.Common.Requests;
using IDL.Service.Common.Responses;
using IDL.Service.Queries.Interfaces;
using IDL.Service.Queries.Parameters;

using ServiceStack;

namespace IDL.Service.Services
{
    public class UserService
    {
        private readonly ICreateUserCommand _createUserCommand;

        private readonly IUserByEmailQuery _userByEmailQuery;

        private readonly IDeleteUserCommand _deleteUserCommand;

        public UserService(
            IUserByEmailQuery userByEmailQuery,
            ICreateUserCommand createUserCommand,
            IDeleteUserCommand deleteUserCommand)
        {
            _userByEmailQuery = userByEmailQuery;
            _createUserCommand = createUserCommand;
            _deleteUserCommand = deleteUserCommand;
        }

        public CreateUserResponse Post(CreateUser request)
        {
            var userResult = _userByEmailQuery.Execute(new UserByEmailParameters { Email = request.Email });

            if (userResult != null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "User already exists");
            }

            var createResult = _createUserCommand.Execute(
                new CreateUserParameters
                {
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password
                });

            return new CreateUserResponse
            {
                UserId = createResult.UserId
            };
        }

        public DeleteUserResponse Delete(DeleteUser request)
        {
            _deleteUserCommand.Execute(
                new DeleteUserParameters
                {
                    UserId = request.UserId
                });

            return new DeleteUserResponse();
        }
    }
}
