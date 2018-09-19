using Microsoft.EntityFrameworkCore;

namespace AuditExams.Models
{
    public class ExamContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<User> Users { get; set; }

        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


    }
}
