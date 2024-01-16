using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Commands;
using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DBI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthCommand authCommand;
        private readonly IAuthQuery authQuery;
        private readonly IMapper mapper;
        public AuthService(IAuthCommand authCommand, IAuthQuery authQuery, IMapper mapper)
        {
            this.authCommand = authCommand;
            this.authQuery = authQuery;
            this.mapper = mapper;
        }
        public async Task<string> AddUserAsync(UserRegisterDto userRegisterDto)
        {
            try
            {
                User user = mapper.Map<User>(userRegisterDto);

                user.Username = user.Identifier;

                var userRes = await authCommand.AddAsync(user);
                await authCommand.SaveChangesAsync();

                return userRes.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return mapper.Map<IEnumerable<UserDto>>(authQuery.GetAll());
        }
        public static string DecodeAuthToken(string authToken)
        {
            //TODO: Save key
            var key = Encoding.ASCII.GetBytes("nC4HGoTRMvgUAU52eHmhEMaQdpmpEwCj0wp6NdGbfqk");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(authToken, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var uidClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                    if (uidClaim != null)
                    {
                        return uidClaim.Value;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd walidacji tokenu: {ex.Message}");
            }
        }
    }
}
