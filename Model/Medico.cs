using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace SistemaAgendamentoConsulta.Model;

public class Medico  : Base
{
    public string Crm { get; set; }
    public int EspecialidadeId { get; set; }
    public DateTime DataHoraInicioAtendimentos { get; set; }
    public DateTime DataHoraFimAtendimentos { get; set; }
}