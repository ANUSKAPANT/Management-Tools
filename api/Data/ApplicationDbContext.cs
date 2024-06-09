using System;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public DbSet<ProjectProfile> ProjectProfiles { get; set; }
        public DbSet<TodoProfile> TodoProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Project>()
                .HasMany(p => p.Todos)
                .WithOne(t => t.Project)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Project>()
                .HasMany(p => p.ProjectProfiles)
                .WithOne(pp => pp.Project)
                .OnDelete(DeleteBehavior.ClientCascade);

             builder.Entity<Todo>()
                .HasMany(t => t.TodoProfiles)
                .WithOne(t => t.Todo)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ProjectProfile>(x => x.HasKey(p => new { p.AppUserId, p.ProjectId }));
            builder.Entity<ProjectProfile>()
            .HasOne(u => u.AppUser)
            .WithMany(p => p.ProjectProfiles)
            .HasForeignKey(p => p.AppUserId);

            builder.Entity<ProjectProfile>()
            .HasOne(s => s.Project)
            .WithMany(p => p.ProjectProfiles)
            .HasForeignKey(p => p.ProjectId);

            builder.Entity<TodoProfile>(x => x.HasKey(p => new { p.AppUserId, p.TodoId }));
            builder.Entity<TodoProfile>()
            .HasOne(u => u.AppUser)
            .WithMany(p => p.TodoProfiles)
            .HasForeignKey(p => p.AppUserId);

            builder.Entity<TodoProfile>()
            .HasOne(s => s.Todo)
            .WithMany(p => p.TodoProfiles)
            .HasForeignKey(p => p.TodoId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                Name = "Admin",
                NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                Name = "User",
                NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }

}

