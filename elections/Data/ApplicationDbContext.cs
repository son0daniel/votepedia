using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace elections.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Macroregion> Macroregions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<MetropolitanArea> MetropolitanAreas { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<CandidateParty> CandidateParties { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<ElectionRound> ElectionRounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
