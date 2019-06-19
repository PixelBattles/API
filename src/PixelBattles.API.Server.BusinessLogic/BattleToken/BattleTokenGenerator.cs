using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelBattles.API.Server.BusinessLogic.BattleToken
{
    public class BattleTokenGenerator : IBattleTokenGenerator
    {
        private readonly SymmetricSecurityKey SecurityKey;
        private readonly JwtSecurityTokenHandler JwtTokenHandler;
        private readonly BattleTokenOptions BattleTokenOptions;

        public BattleTokenGenerator(IOptions<BattleTokenOptions> battleTokenOptions)
        {
            BattleTokenOptions = battleTokenOptions.Value ?? throw new ArgumentNullException(nameof(battleTokenOptions));

            JwtTokenHandler = new JwtSecurityTokenHandler();
            SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(BattleTokenOptions.SecretKey));
        }

        public string GenerateToken(long battleId, Guid userId)
        {
            var claims = new[] {
                new Claim(BattleTokenConstants.BattleIdClaim, battleId.ToString()),
                new Claim(BattleTokenConstants.UserIdClaim, userId.ToString())
            };

            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: BattleTokenOptions.DefaultIssuer,
                audience: BattleTokenOptions.DefaultAudience,
                claims: claims,
                notBefore: null,
                expires: DateTime.UtcNow.Add(BattleTokenOptions.DefaultTimeLife), 
                signingCredentials: credentials);

            return JwtTokenHandler.WriteToken(token);
        }
    }
}
