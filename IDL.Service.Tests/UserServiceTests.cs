using System;

using FluentAssertions;

using IDL.Service.Commands.Interfaces;
using IDL.Service.Commands.Parameters;
using IDL.Service.Common.Requests;
using IDL.Service.Models;
using IDL.Service.Queries.Interfaces;
using IDL.Service.Queries.Parameters;
using IDL.Service.Services;

using NSubstitute;

using NUnit.Framework;

using ServiceStack;

namespace IDL.Service.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserByEmailQuery _userByEmailQuery;

        private ICreateUserCommand _createUserCommand;

        private IDeleteUserCommand _deleteUserCommand;

        [SetUp]
        public void Setup()
        {
            _userByEmailQuery = Substitute.For<IUserByEmailQuery>();
            _createUserCommand = Substitute.For<ICreateUserCommand>();
            _deleteUserCommand = Substitute.For<IDeleteUserCommand>();
        }

        [Test]
        public void HttpErrorThrownWhenCreatingUserAlreadyExists()
        {
            // Arrange
            _userByEmailQuery
                .Execute(Arg.Any<UserByEmailParameters>())
                .Returns(new VouchercloudUser());

            var service = new UserService(_userByEmailQuery, _createUserCommand, _deleteUserCommand);

            var request = new CreateUser
            {
                DateOfBirth = new DateTime(1980, 1, 1),
                Email = "james@vouchercloud.com",
                FirstName = "James",
                LastName = "Harper",
                Password = "Password"
            };

            // Act, Assert
            service
                .Invoking(s => s.Post(request))
                .ShouldThrow<HttpError>().And.Message.Should().Be("User already exists");

            _createUserCommand
                .DidNotReceive()
                .Execute(Arg.Any<CreateUserParameters>());
        }

        [Test]
        public void CanCreateUserWhenUserNotFound()
        {
            // Arrange
            _userByEmailQuery
                .Execute(Arg.Any<UserByEmailParameters>())
                .Returns((VouchercloudUser)null);

            _createUserCommand.Execute(Arg.Any<CreateUserParameters>())
                .Returns(
                    new VouchercloudUser
                    {
                        UserId = 1
                    });

            var service = new UserService(_userByEmailQuery, _createUserCommand, _deleteUserCommand);

            var request = new CreateUser
                          {
                              DateOfBirth = new DateTime(1980, 1, 1),
                              Email = "james@vouchercloud.com",
                              FirstName = "James",
                              LastName = "Harper",
                              Password = "Password"
                          };
            // Act
            var response = service.Post(request);

            // Assert
            _createUserCommand
                .Received(1)
                .Execute(Arg.Any<CreateUserParameters>());

            response.Should().NotBeNull();
            response.UserId.Should().Be(1);
        }

        
    }
}
