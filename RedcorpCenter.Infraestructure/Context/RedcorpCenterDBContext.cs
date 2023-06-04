using Microsoft.EntityFrameworkCore;
using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure.Context
{
    public class RedcorpCenterDBContext:DbContext
    {
        public RedcorpCenterDBContext()
        {

        }

        public RedcorpCenterDBContext(DbContextOptions<RedcorpCenterDBContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
                optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;Pwd=12345678;Database=RedcorpCenterDB;", serverVersion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Section>().ToTable("Sections");
            modelBuilder.Entity<Section>().HasKey(p => p.Id);
            modelBuilder.Entity<Section>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Section>().Property(p => p.Description).IsRequired().HasMaxLength(70);

            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasKey(p => p.Id);
            modelBuilder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(p => p.IsActive).HasDefaultValue(true);
            
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().HasKey(p => p.Id);
            modelBuilder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>().Property(p => p.Description).IsRequired().HasMaxLength(70);
            modelBuilder.Entity<Project>().Property(p => p.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Project>().Property(p => p.InitialDate).HasDefaultValue(DateTime.Now);
        }
    }
}
