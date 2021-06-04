using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Authentication.API.Domain.Models;
using Authentication.API.Domain.Services;
using Authentication.API.Domain.Services.Communication;
using Authentication.API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.API.Services
{
    public class UserService: IUserService
    {
        // TODO: Replace by Repository-based Implementation
        private List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "john.doe@gmail.com",
                Password = "test"
            },
            new User
            {
                Id = 2,
                FirstName = "Jason",
                LastName = "Bourne",
                Username = "jason.bourne@treatstone.gov",
                Password = "password"

            }
        };

        private readonly AppSettings _appSettings;


        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var user = _users.SingleOrDefault(x =>
                x.Username == request.Username &&
                x.Password == request.Password);

            if (user == null) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
