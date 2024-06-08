using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SistemaAgendamentoConsulta.Context;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.IRepository;
using SistemaAgendamentoConsulta.Model;
using System.Security.Authentication;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SistemaAgendamentoConsulta.Repository;

public class MedicoRepository : Repository<Medico>, IMedicoRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public MedicoRepository(AppDbContext appDbContext, IMapper mapper) : base(appDbContext)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Medico> MedicoExistAsync(LoginMedicoDTO loginMedicoDto)
    {
        return await _appDbContext.Medico
            .Where(x => x.Email == loginMedicoDto.Email)
            .FirstOrDefaultAsync();
    }

    public async Task<Medico> GetMedicoByIdAsync(int id)
    {
        return Get(c => c.Id == id);
    }

    public List<MedicoResponseDTO> ObterMedicosResponse()
    {
        
        return _appDbContext.Medico.Select(x => new MedicoResponseDTO()
        {
            Nome = x.Nome,
            Crm = x.Crm,
            Cpf = x.Cpf
        }).AsNoTracking().ToList();
    
        //return _appDbContext.Medico.Select(p => new { p.Nome, p.Email, p.Telefone, p.Crm });
    }

    public async Task<Medico> CreateMedico(CreateMedicoDTO createMedicoDto)
    {
        var senha = BCryptNet.HashPassword(createMedicoDto.Senha);
        createMedicoDto.Senha = senha;
        Medico medico = _mapper.Map<Medico>(createMedicoDto);
        medico.Role = Role.Medico;
        return Create(medico);
    }
    
    public async Task<UpdateMedicoDTOResponse> UpdateMedico(int id, JsonPatchDocument<UpdateMedicoDTORequest> patchMedicoDTO, Medico medico)
    {
        var medicoMapper = _mapper.Map<UpdateMedicoDTORequest>(medico);
        //aplica as alterações definidas no documento JSON Patch ao objeto Request medicoMapper
        patchMedicoDTO.ApplyTo(medicoMapper);
        // Mapeia as alterações de volta para a entidade Medico
        _mapper.Map(medicoMapper, medico);

        Update(medico);
       var response = _mapper.Map<UpdateMedicoDTOResponse>(medico);
       return response;
    }
    
    


}