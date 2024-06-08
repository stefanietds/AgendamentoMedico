using SistemaAgendamentoConsulta.Controller;
using SistemaAgendamentoConsulta.DTO.Mapping;
using Consulta = SistemaAgendamentoConsulta.Model.Consulta;

namespace SistemaAgendamentoConsulta.IService;

public interface IConsultaService
{
    Task<List<Consulta>> GetAllConsultas(int idPaciente);
    Task<Consulta> CreateConsulta(ConsultaCreateDTO consultaDTO);
}