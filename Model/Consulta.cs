using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;

namespace SistemaAgendamentoConsulta.Model;

public class Consulta
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; }
    public int MedicoId { get; set; }
    public Medico Medico { get; set; }
    public DateTime DataHora { get; set; }
    public string? Descricao { get; set; }
    public Status StatusConsulta { get; set; }
}
