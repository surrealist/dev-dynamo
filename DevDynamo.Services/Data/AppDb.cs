using DevDynamo.Models;
using Microsoft.EntityFrameworkCore;

namespace DevDynamo.Services.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
            // 
             
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<WorkflowStep> WorkflowSteps { get; set; } = null!;
    }
}
