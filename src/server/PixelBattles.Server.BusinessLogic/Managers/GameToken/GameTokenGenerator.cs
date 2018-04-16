using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public class GameTokenGenerator : IGameTokenGenerator
    {
        private readonly SymmetricSecurityKey SecurityKey;
        private readonly JwtSecurityTokenHandler JwtTokenHandler;
        private readonly GameTokenOptions GameTokenOptions;

        public GameTokenGenerator(IOptions<GameTokenOptions> gameTokenOptions)
        {
            GameTokenOptions = gameTokenOptions.Value ?? throw new ArgumentNullException(nameof(gameTokenOptions));

            JwtTokenHandler = new JwtSecurityTokenHandler();
            SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GameTokenOptions.SecretKey));
        }

        public string GenerateToken(Guid gameId, Guid userId)
        {
            var claims = new[] {
                new Claim(GameTokenConstants.GameIdClaim, gameId.ToString()),
                new Claim(GameTokenConstants.UserIdClaim, userId.ToString())
            };

            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: GameTokenOptions.DefaultIssuer,
                audience: GameTokenOptions.DefaultAudience,
                claims: claims,
                notBefore: null,
                expires: DateTime.UtcNow.Add(GameTokenOptions.DefaultTimeLife), 
                signingCredentials: credentials);

            return JwtTokenHandler.WriteToken(token);
        }
    }
}
