using Microsoft.EntityFrameworkCore;
using System;
using ПР45_Осокин.Model;

namespace ПР45_Осокин.Context
{
    public class TaskContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

        public TaskContext()
        {
            Database.EnsureCreated();
            Tasks.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql("server=localhost;" + "port=3306;" + "uid=root;" + "pwd=;" +"database=TaskManager",
                new MySqlServerVersion(new Version(8, 0, 11)));
    }
}
