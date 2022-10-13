using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;


namespace la_mia_pizzeria_post.Models
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }
        public Context()
        {

        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Pizza> Pizza { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzeria-db;Integrated Security=True");
        }
    }
}
