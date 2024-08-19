

using Shared.Lib.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Lib.Utilities.Authentication
{
    public static class JWTHelper
    {
        public static CustomUserClaim DecryptToken(string jwtToken) {

            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var userId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var role = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if(userId is null || name is null || email is null || role is null) return new CustomUserClaim();

            return new CustomUserClaim(userId.Value, name.Value, email.Value, role.Value);
        }

        public static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaim customUserClaim)
        {

            if (customUserClaim is null) return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, customUserClaim.Name),
                    new(ClaimTypes.Name, customUserClaim.Name),
                    new(ClaimTypes.Email, customUserClaim.Email),
                    new(ClaimTypes.Role, customUserClaim.Role),
                }
            , "JwtAuth"));

        }
    }
}
