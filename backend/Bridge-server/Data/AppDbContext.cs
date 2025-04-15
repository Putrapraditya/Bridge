using Microsoft.EntityFrameworkCore;
using Bridge_server.Entities;
using Boilerplate.Entities;

namespace Bridge_server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MsUsersC> MsUsersC { get; set; }

        public DbSet<MsTenant> MsTenant { get; set; }
        public DbSet<MsProject> MsProject { get; set; }
        public DbSet<MsTenantProject> MsTenantProject { get; set; }
        public DbSet<MsUserTenant> MsUserTenant { get; set; }



    }
}
