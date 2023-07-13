using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Data
{
    public class appDbContext : IdentityDbContext<User>
    {
        public appDbContext(DbContextOptions<appDbContext> opts) : base(opts) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Adresses { get; set; }
    }
}
