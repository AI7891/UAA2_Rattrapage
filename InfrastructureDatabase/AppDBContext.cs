using DomainEntityModels.Enums;
using DomainEntityModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InfrastructureDatabase
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }
        // DbSet for the Member entity, allowing CRUD operations on the Members table in the database.
        public DbSet<Member> Members { get; set; } = null!;

        // Override the OnModelCreating method to configure the model and register the MemberStatus enum with PostgreSQL, as well as apply all entity configurations from the assembly.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Register the MemberStatus enum with PostgreSQL, enabling it to be used as a column type in the database.
            modelBuilder.HasPostgresEnum<MemberStatus>("MemberStatus");

            // Apply all entity configurations defined in the same assembly as AppDBContext, allowing for a modular and organized approach to configuring the database schema.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }

    }
}
