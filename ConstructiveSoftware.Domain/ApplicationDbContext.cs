using ConstructiveSoftware.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructiveSoftware.Domain
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Area> Areas { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
