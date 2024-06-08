using System.Runtime.InteropServices.JavaScript;

namespace SistemaAgendamentoConsulta.DTO.Mapping;

public class ConsultaCreateDTO
{
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public DateTime DataEHoraConsulta { get; set; }
}