using DBI.Application.Commands;
using DBI.Application.Queries;
using DBI.Infrastructure.Commands;
using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Providers;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services.Authorization;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using SixLabors.ImageSharp.Formats.Qoi;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DBI.Application.Services.Authorization
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private readonly IJwtProvider jwtProvider;
        private readonly FirebaseAuthClient client;
        private readonly IAuthCommand authCommand;
        private readonly IAuthQuery authQuery;

        public FirebaseAuthService(IJwtProvider jwtProvider, FirebaseAuthClient client, IAuthCommand authCommand, IAuthQuery authQuery)
        {
            this.jwtProvider = jwtProvider;
            this.client = client;
            this.authCommand = authCommand;
            this.authQuery = authQuery;
        }

        public async Task<string?> LoginAsync(Infrastructure.Dto.UserCredential dto)
        {
            var userArgs = new UserRecordArgs
            {
                Email = dto.Email,
                Password = dto.Password
            };

            var credential = await client.SignInWithEmailAndPasswordAsync(dto.Email, dto.Password);
            string userId = credential.User.Uid;

            var userInDb = await authQuery.GetByIdAsync(userId);

            var claimsDict = new Dictionary<string, object>();
            claimsDict.Add("username", userInDb.Username);
            claimsDict.Add("email", userInDb.Identifier);

            //return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userId, claimsDict);

            return await jwtProvider.GetForCredentialsAsync(credential.User.Info.Email, dto.Password);
        }

        public async Task<string?> SignUpAsync(Infrastructure.Dto.UserCredential dto)
        {
            var userCredentials = await client.CreateUserWithEmailAndPasswordAsync(dto.Email, dto.Password);

            if (userCredentials == null)
                return null;

            await authCommand.AddAsync(new Domain.Entities.Core.User()
            {
                Identifier = dto.Email,
                Role = "User",
                Username = dto.Email.Substring(0, dto.Email.IndexOf('@')),
                Id = userCredentials.User.Uid
            });
            await authCommand.SaveChangesAsync();
            return await userCredentials.User.GetIdTokenAsync();
        }

        public async static Task<string> GetUserIdByBearerToken(string bearerToken)
        {
            string token = bearerToken.Split(" ")[1];
            return (await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token)).Uid;
        }
    }
}
