using System.Security.Claims;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.IService;

public interface IMedicoService
{
    Task<TokenResponse> Authenticate(LoginMedicoDTO medico);
    Task<object> GetAllMedicos();
    List<MedicoResponseDTO> ObterMedicosResponse();

    Task<Medico> CreateMedico(CreateMedicoDTO createMedicoDto);
    Task<UpdateMedicoDTOResponse> UpdateMedico(int id, JsonPatchDocument<UpdateMedicoDTORequest> patchMedicoDTO);
}