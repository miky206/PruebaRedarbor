using Microsoft.EntityFrameworkCore;
using testRA.Domain.Entities;

namespace testRA.Infrastructure.Contexts
{
    public class TestRedarborContext : DbContext
    {
        public TestRedarborContext(DbContextOptions<TestRedarborContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidates>()
                .HasAlternateKey(c => c.Email)
                .HasName("AlternateKey_Email");
        }
        public DbSet<Candidates> Candidates { get; set; } = default!;
        public DbSet<CandidateExperience> CandidateExperience { get; set; } = default!;
    }
}
