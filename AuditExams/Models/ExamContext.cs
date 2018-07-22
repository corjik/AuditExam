using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditExams.Models
{
    public class ExamContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }

        public ExamContext(DbContextOptions<ExamContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
