using Microsoft.EntityFrameworkCore;
using static ExamTicketsAppWPF.ConnectionDb.Connection;


namespace ExamTicketsAppWPF.Models
{
	public sealed class TicketsDbContext : DbContext
	{
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Practice> practices { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Category> categories { get; set; }

        public TicketsDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=GR-OFFC\SQLEXPRESS;Database=TicketsDb;Trusted_Connection=True;");
            }
        }
    }
}
