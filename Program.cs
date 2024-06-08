using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaAgendamentoConsulta.Context;
using SistemaAgendamentoConsulta.DTO;
using SistemaAgendamentoConsulta.DTO.Mapping;
using SistemaAgendamentoConsulta.IRepository;
using SistemaAgendamentoConsulta.IService;
using SistemaAgendamentoConsulta.Repository;
using SistemaAgendamentoConsulta.Service;
using SistemaAgendamentoConsulta.Validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}).AddNewtonsoftJson();

builder.Services.AddScoped<IValidator<CreateMedicoDTO>, CreateMedicoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMedicoDTOValidator>();

builder.Services.AddScoped<IValidator<LoginMedicoDTO>, LoginMedicoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginMedicoDTOValidator>();


//builder.Services.Configure<ApiBehaviorOptions>(options
//    => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
                //Scheme = "JWT",
                //Name = "Bearer",
                //In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();

string sqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(sqlServerConnection));

var secret = builder.Configuration["JWT:SecretKey"];
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    //o.IncludeErrorDetails = true;
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,            
        ClockSkew = TimeSpan.Zero,           
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IMedicoService, MedicoService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();