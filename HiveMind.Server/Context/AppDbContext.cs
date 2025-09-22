using HiveMind.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HiveMind.Server.Context
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration configuration;
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Drone> Drones { get; set; }
    }
}
