using SistemaAgendamentoConsulta.Controller;
using SistemaAgendamentoConsulta.DTO.Mapping;
using Consulta = SistemaAgendamentoConsulta.Model.Consulta;

namespace SistemaAgendamentoConsulta.IRepository;

public interface IConsultaRepository
{
    Task<List<Consulta>> GetAllConsultas(int idPaciente);
    Task<Consulta> GetConsulta(int idConsulta);

    Task<bool> DisponibilidadeConsulta(ConsultaCreateDTO consultaDTO);
    //Task<Consulta> CreateConsulta(ConsultaCreateDTO consultaDTO);
}