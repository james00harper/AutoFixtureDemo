using System;

namespace IDL.Service.Common.Requests
{
    public class CreateUser
    {
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
