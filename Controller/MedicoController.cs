using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.IService;

namespace SistemaAgendamentoConsulta.Controller;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : ControllerBase
{
    private readonly IMedicoService _medicoService;
    private IConfiguration _configuration;

    public MedicoController(IMedicoService medicoService, IConfiguration configuration)
    {
        _medicoService = medicoService;
        _configuration = configuration;
    }


    [HttpGet("[action]")]
    [Authorize]
    public object GetMedicos()
    {
        return Ok(_medicoService.GetAllMedicos());
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ObterMedicosResponse()
    {
        var medico = _medicoService.ObterMedicosResponse();
        if (medico == null)
            return NotFound();

        //return Ok(medico.Select(p => new {p.Nome, p.Crm}));
        return Ok(medico);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<ActionResult> CreateMedico(CreateMedicoDTO createMedicoDto)
    {
        try
        {
            var medico = await _medicoService.CreateMedico(createMedicoDto);
            if (medico == null)
                return NoContent();

            return Ok(medico);
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


    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> PatchMedico(int id, [FromBody] JsonPatchDocument<UpdateMedicoDTORequest> patchMedicoDTO)
    {
        try
        {
            if (patchMedicoDTO == null || id == null)
                return BadRequest();

            var medico = await _medicoService.UpdateMedico(id, patchMedicoDTO);
            if (medico == null)
                return NoContent();

            return Ok(medico);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.StackTrace}");
        }
    }
    
}