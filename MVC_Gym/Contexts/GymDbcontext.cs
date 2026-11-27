using Microsoft.EntityFrameworkCore;
using MVC_Gym.Entities;
using System.Reflection;

namespace MVC_Gym.Contexts
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Traineer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Seasion> Sessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
    }
}