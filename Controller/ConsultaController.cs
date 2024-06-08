using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoConsulta.DTO.Mapping;
using SistemaAgendamentoConsulta.IService;

namespace SistemaAgendamentoConsulta.Controller;

[Route("api/[controller]")]
[ApiController]
public class ConsultaController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IConsultaService _consultaService;

    public ConsultaController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }
    
    [Authorize]
    [HttpGet]
    [Authorize(Roles = "Paciente")]
    public async Task<OkObjectResult> GetConsulta(int pacienteId)
    {
        return Ok(await _consultaService.GetAllConsultas(pacienteId));
    }


    [HttpPost]
    [Authorize(Roles = "Medico")]
    public async Task<OkObjectResult> PostConsulta(ConsultaCreateDTO consultaDTO)
    {
        return Ok(await _consultaService.CreateConsulta(consultaDTO));
    }

}