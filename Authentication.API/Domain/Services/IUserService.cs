using System;
using System.Collections.Generic;
using Authentication.API.Domain.Models;
using Authentication.API.Domain.Services.Communication;

namespace Authentication.API.Domain.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        IEnumerable<User> GetAll();
    }
}
