using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SistemaAgendamentoConsulta.IService;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims,
        IConfiguration _configuration);
}