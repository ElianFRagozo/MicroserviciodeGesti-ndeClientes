using MicroserviciodeGestióndeClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviciodeGestióndeClientes.Data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> clientes { get; set; }
    }
}
