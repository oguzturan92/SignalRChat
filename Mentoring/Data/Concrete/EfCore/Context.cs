using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class Context : IdentityDbContext<ProjeUser, ProjeRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=mentoring;Integrated Security=True;");
        }

        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staj> Stajs { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatLine> ChatLines { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}