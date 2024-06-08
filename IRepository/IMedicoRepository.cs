using Microsoft.AspNetCore.JsonPatch;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.DTO.Mapping;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.IRepository;

public interface IMedicoRepository : IRepository<Medico>
{
    Task<Medico> MedicoExistAsync(LoginMedicoDTO loginMedicoDto);
     Task<Medico> GetMedicoByIdAsync(int id);
     List<MedicoResponseDTO> ObterMedicosResponse();
     Task<Medico> CreateMedico(CreateMedicoDTO createMedicoDto);
     Task<UpdateMedicoDTOResponse> UpdateMedico(int id, JsonPatchDocument<UpdateMedicoDTORequest> patchMedicoDTO, Medico medico);
}