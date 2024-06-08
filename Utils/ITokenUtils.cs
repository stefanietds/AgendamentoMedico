using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.Utils
{
    public interface ITokenUtils
    {
        string GenerateAccessToken(Task<Medico> medico, string secret);
        string GenerateRefreshToken();
        string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret);
    }
}
