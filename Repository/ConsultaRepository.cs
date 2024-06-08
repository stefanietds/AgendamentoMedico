using Microsoft.EntityFrameworkCore;
using SistemaAgendamentoConsulta.Context;
using SistemaAgendamentoConsulta.DTO.Mapping;
using SistemaAgendamentoConsulta.IRepository;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.Repository;

public class ConsultaRepository : Repository<Consulta>,IConsultaRepository
{
    private readonly AppDbContext _appDbContext;

    public ConsultaRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<Consulta>> GetAllConsultas(int idPaciente)
    {
        var consultas = await _appDbContext.Consultas
            .Include(u => u.Medico.Nome)
            .Where(x => x.PacienteId == idPaciente)
            .OrderBy(x => x.DataHora)
            .AsNoTracking()
            .ToListAsync();

        return consultas;
    }
    
     
     
    public async Task<Consulta> GetConsulta(int idConsulta)
    {
        var consulta = await _appDbContext.Consultas.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == idConsulta);

        return consulta;
    }

    
    public async Task<bool> DisponibilidadeConsulta(ConsultaCreateDTO consultaDTO)
    {
        
        var disponibilidade = await _appDbContext.Medico
            .FirstOrDefaultAsync(a => a.Id == consultaDTO.MedicoId && 
                                      a.DataHoraInicioAtendimentos >= consultaDTO.DataEHoraConsulta &&
                                      a.DataHoraFimAtendimentos <= consultaDTO.DataEHoraConsulta);

        if (disponibilidade == null)
            return false;
        
        //procedure
        var consultaExistente = await _appDbContext.Consultas
            .FirstOrDefaultAsync(c => c.MedicoId == consultaDTO.MedicoId 
                                      && c.DataHora <= consultaDTO.DataEHoraConsulta);

        if (consultaExistente != null)
        {
            return false; 
        }

        return true;
    }
    
    

}