using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_post.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
        public Ingredient()
        {

        }
    }
}
