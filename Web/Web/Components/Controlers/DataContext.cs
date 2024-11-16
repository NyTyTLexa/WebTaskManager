using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Web.Components.Controlers
{
    public class DataContext : DbContext
    {

        public DbSet<Model.Priority>? Priority { get; set; }
        public DbSet<Model.Status>? Status { get; set; }
        public DbSet<Model.Task>? Task { get; set; }
        public DbSet<Model.User>? User { get; set; }
        public DbSet<Model.UserAndTask>? UserAndTask { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./DBTaskManager.db");
        }
    }
}
