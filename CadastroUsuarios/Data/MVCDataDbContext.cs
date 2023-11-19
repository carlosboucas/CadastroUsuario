using CadastroUsuarios.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuarios.Data
{
    public class MVCDataDbContext : DbContext
    {
        public MVCDataDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
