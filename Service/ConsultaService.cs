using SistemaAgendamentoConsulta.DTO.Mapping;
using SistemaAgendamentoConsulta.IRepository;
using SistemaAgendamentoConsulta.IService;
using SistemaAgendamentoConsulta.Model;
using SistemaAgendamentoConsulta.Repository;
using SistemaAgendamentoConsulta.Validator;

namespace SistemaAgendamentoConsulta.Service;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _consultaRepository;

    public ConsultaService(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public Task<List<Consulta>> GetAllConsultas(int idPaciente)
    {
        var consulta = _consultaRepository.GetAllConsultas(idPaciente);
        if (consulta is null)
            return null;

        return consulta;
    }

    public async Task<Model.Consulta> CreateConsulta(ConsultaCreateDTO consultaDTO)
    {
        var cons = new ConsultaValidator();
        
        var result = cons.Validate(consultaDTO);

        if (!result.IsValid) 
            return null; 
        
        var disponibilidade = await _consultaRepository.DisponibilidadeConsulta(consultaDTO);
        if (!disponibilidade)
            return null; 
        
       // var criarConsulta = await _consultaRepository.CreateConsulta(consultaDTO);
       //return criarConsulta;
       throw new Exception();
    }



}