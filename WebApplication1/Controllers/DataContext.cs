﻿using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApplication1.Controllers
{
    public class DataContext:DbContext
    {

        public DbSet<Model.Priority> Priority { get; set; }
        public DbSet<Model.Status> Status { get; set; }
        public DbSet<Model.Task> Task { get; set; }
        public DbSet<Model.Users> USER { get; set; }
        public DbSet<Model.UserAndTask> UserandTask { get; set; }
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=DBTaskManager.db");
        }
    }
}
