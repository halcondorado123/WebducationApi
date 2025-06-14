using BCrypt.Net;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebducationApi.Application.DTO.Token;
using WebducationApi.Domain.Entities.Token;
using WebducationApi.Infraestructure.Data;
using Microsoft.Extensions.Options;

namespace WebducationApi.Modules
{
    public class JWTExtension
    {
        private readonly ApiDbContext _dbContext;
        private readonly InitUserOptions _initUser;
        private readonly IConfiguration _configuration;

        public JWTExtension(ApiDbContext dbContext, IConfiguration configuration, IOptions<InitUserOptions> initUserOptions)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _initUser = initUserOptions.Value;
        }

        public async Task<LoginResponseModel?> Authenticate(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            using var connection = _dbContext.CreateSqlConnection();

            // Verificar si existe el usuario
            // Buscar usuario
            var user = await connection.QueryFirstOrDefaultAsync<UserDto>(
                "SELECT UserName, UserPasswordHash FROM [EDU].[Users] WHERE UserName = @UserName",
                new { request.UserName }
            );

            if (user == null)
            {
                if (request.UserName == _initUser.UserName && request.Password == _initUser.Password)
                {
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword(_initUser.Password);

                    await connection.ExecuteAsync(
                        "INSERT INTO [EDU].[Users] (UserName, UserPasswordHash) VALUES (@UserName, @PasswordHash)",
                        new { UserName = _initUser.UserName, PasswordHash = passwordHash }
                    );

                    user = new UserDto { UserName = _initUser.UserName, UserPasswordHash = passwordHash };
                }
                else
                {
                    return null;
                }
            }

            // Si llegamos aquí, ya hay un usuario válido
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.UserPasswordHash))
                return null;

            // Crear JWT
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiry = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Name, request.UserName)
        }),
                Expires = tokenExpiry,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new LoginResponseModel
            {
                AccessToken = accessToken,
                UserName = request.UserName,
                ExpiresIn = (int)(tokenExpiry - DateTime.UtcNow).TotalSeconds
            };
        }

    }
}
