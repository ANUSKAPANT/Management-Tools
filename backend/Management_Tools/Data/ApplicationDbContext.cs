using System;
using Microsoft.EntityFrameworkCore;
using Management_Tools.Models;

namespace Management_Tools.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Todo> Todos { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure relationships
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Projects)
        //        .WithMany(p => p.Users)
        //        .UsingEntity(j => j.ToTable("UserProject"));

        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Todos)
        //        .WithMany(p => p.Users)
        //        .UsingEntity(j => j.ToTable("TaskAssignment"));


        //    modelBuilder.Entity<Todo>()
        //         .HasOne<Project>()
        //         .WithMany(p => p.Users)
        //         .HasForeignKey(t => t.ProjectId);

        //}
    }

}

