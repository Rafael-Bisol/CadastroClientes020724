using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroClientes.Models;

namespace CadastroClientes.Data
{
    public class AppDbContexto : DbContext
    {
        // tbClientes -> nome da tabela no banco de dados
        public DbSet<Cliente> tbClientes{ get; set; }
        public AppDbContexto (DbContextOptions<AppDbContexto> options) : base(options)
        {
        }
        // define a chave primária pro EntityFrameworkCore
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(c => c.Cliente_id);
        }
    }
}