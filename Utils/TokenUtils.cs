using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.Utils;

public static class TokenUtils
{
    public static string GenerateAccessToken(Medico medico, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            //Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, medico.Email) }),
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Email, medico.Email),
                new Claim(ClaimTypes.Role, "medico")
            }),
            Expires = DateTime.UtcNow.AddMinutes(45),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public static string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(3), // Extend expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}