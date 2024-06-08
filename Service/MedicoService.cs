using System.Security.Authentication;
using Microsoft.AspNetCore.JsonPatch;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.IRepository;
using SistemaAgendamentoConsulta.IService;
using SistemaAgendamentoConsulta.Model;
using SistemaAgendamentoConsulta.Utils;
using SistemaAgendamentoConsulta.Validator;



namespace SistemaAgendamentoConsulta.Service;

public class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _medicoRepository;
    private readonly IConfiguration _configuration;

    public MedicoService(IMedicoRepository medicoRepository, IConfiguration configuration)
    {
        _medicoRepository = medicoRepository;
        _configuration = configuration;
    }

    public async Task<bool> MedicoIsValid(LoginMedicoDTO _loginMedicoDto)
    {
        var medico = await _medicoRepository.MedicoExistAsync(_loginMedicoDto);
        if(medico != null)
            return true;
        return false;
    }

    public async Task<TokenResponse> Authenticate(LoginMedicoDTO loginMedicoDTO)
    {
        if(loginMedicoDTO == null)
            throw new ArgumentNullException();
        
        var validator = new LoginMedicoDTOValidator();
        var valid = validator.Validate(loginMedicoDTO);

        if (!valid.IsValid)
        {
            var errorMessage = string.Join("\n", valid.Errors
                .Select(error => error.ErrorMessage).ToList());
            throw new BadHttpRequestException(errorMessage);
        }

        var medico = await _medicoRepository.MedicoExistAsync(loginMedicoDTO);

        if (medico == null || !ValidatePassword(medico.Senha, loginMedicoDTO.Senha))
            throw new AuthenticationException();

        var accessToken = TokenUtils.GenerateAccessToken(medico, _configuration["JWT:SecretKey"]);
        var refreshToken = TokenUtils.GenerateRefreshToken();

        var response = new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        
        return response;
    }

    private bool ValidatePassword(string senha1, string senha2)
    {
        return BCrypt.Net.BCrypt.Verify(senha1, senha2);
    }

    public async Task<object> GetAllMedicos()
    {
        var medicos = _medicoRepository.ObterMedicosResponse();
        if (medicos is null)
            return null;

        return medicos;
    }


    public List<MedicoResponseDTO> ObterMedicosResponse()
    {
        return _medicoRepository.ObterMedicosResponse();
    }

    public async Task<Medico> CreateMedico(CreateMedicoDTO createMedicoDto)
    {
        if (createMedicoDto == null)
            throw new ArgumentNullException();

        var validator = new CreateMedicoDTOValidator();
        var valid = validator.Validate(createMedicoDto);

        if (!valid.IsValid)
        {
            var errorMessage = string.Join("\n", valid.Errors
                .Select(error => error.ErrorMessage).ToList());
            throw new BadHttpRequestException(errorMessage);
        }


        var medico = await _medicoRepository.CreateMedico(createMedicoDto);

        return medico;
    }

    public async Task<UpdateMedicoDTOResponse> UpdateMedico(int id, JsonPatchDocument<UpdateMedicoDTORequest> patchMedicoDTO)
    {
        var medicoExist = await _medicoRepository.GetMedicoByIdAsync(id);

        var medico = await _medicoRepository.GetMedicoByIdAsync(id);
        var medicoResponse = await _medicoRepository.UpdateMedico(id, patchMedicoDTO, medico);

        return medicoResponse;
    }
    
    
}