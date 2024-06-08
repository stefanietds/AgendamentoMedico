using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.DTO.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Paciente, PacienteCreateDTO>().ReverseMap();
        CreateMap<Consulta, ConsultaCreateDTO>().ReverseMap();
        CreateMap<Medico, CreateMedicoDTO>().ReverseMap();
        CreateMap<Medico, UpdateMedicoDTORequest>().ReverseMap();
        CreateMap<Medico, UpdateMedicoDTOResponse>().ReverseMap();
        CreateMap<UpdateMedicoDTORequest, UpdateMedicoDTOResponse>().ReverseMap();
        CreateMap<Medico, LoginMedicoDTO>().ReverseMap();
    }
}