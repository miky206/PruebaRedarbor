using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using testRA.Models;

namespace testRA.Data
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
