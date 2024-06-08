using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.IService;
using SistemaAgendamentoConsulta.Utils;
using System.Security.Authentication;

namespace SistemaAgendamentoConsulta.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMedicoService _medicoService;

    public AuthController(IConfiguration configuration, IMedicoService medicoService)
    {
        _configuration = configuration;
        _medicoService = medicoService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Login(LoginMedicoDTO loginMedicoDto)
    {
        try
        {
            var response = await _medicoService.Authenticate(loginMedicoDto);
            if (response == null || string.IsNullOrWhiteSpace(response.AccessToken))
                return BadRequest("Erro ao processar solicitação");

            return Ok(response);
        }
        catch (AuthenticationException ex)
        {
            return Unauthorized($"{ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest($"{ex.Message}");
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest($"{ex.Message}");
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro interno. Por favor, tente novamente mais tarde.");
        }


    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult> Refresh(TokenResponse tokenResponse)
    {
        var newAccessToken = TokenUtils.GenerateAccessTokenFromRefreshToken
            (tokenResponse.RefreshToken, _configuration["JWT:SecretKey"]);

        var response = new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = tokenResponse.RefreshToken 
        };

        return Ok(response);
    }
}