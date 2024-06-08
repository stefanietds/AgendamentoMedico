
using Microsoft.EntityFrameworkCore;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Paciente>? Pacientes { get; set; }
    public DbSet<Medico>? Medicos { get; set; }
    public DbSet<Consulta>? Consultas { get; set; }
    public DbSet<Especialidade>? Especialidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Medico>()
          .Property(p => p.Id)
          .HasColumnType("Id");

        modelBuilder.Entity<Medico>()
          .Property(p => p.Nome)
          .HasMaxLength(50)
          .HasColumnType("nvarchar(50)")
          .IsRequired(); //not null

        modelBuilder.Entity<Medico>()
           .Property(p => p.Role)
           .HasColumnType("nvarchar(10)")
           .HasConversion(typeof(string));

        modelBuilder.Entity<Medico>()
          .Property(p => p.Email)
          .HasMaxLength(50)
          .HasColumnType("nvarchar(50)")
          .IsRequired(); //not null

        modelBuilder.Entity<Medico>()
          .Property(p => p.Senha)
          .HasMaxLength(12)
          .HasColumnType("nvarchar(12)")
          .IsRequired(); //not null

        modelBuilder.Entity<Medico>()
          .Property(p => p.Cpf)
          .HasMaxLength(12)
          .HasColumnType("nvarchar(12)")
          .IsRequired(); //not null

        modelBuilder.Entity<Medico>()
          .Property(p => p.Rg)
          .HasMaxLength(50)
          .HasColumnType("nvarchar(50)")
          .IsRequired(); //not null

        modelBuilder.Entity<Medico>()
          .Property(p => p.Senha)
          .HasMaxLength(12)
          .HasColumnType("nvarchar(12)")
          .IsRequired(); //not null

    }

}