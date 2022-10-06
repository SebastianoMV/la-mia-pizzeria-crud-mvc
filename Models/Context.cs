using Microsoft.EntityFrameworkCore;


namespace la_mia_pizzeria_post.Models
{
    public class Context : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzeria-db;Integrated Security=True");
        }
    }
}
