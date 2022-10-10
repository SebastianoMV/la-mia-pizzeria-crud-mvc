using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_post.Models
{
    public class Pizza
    {
        public Pizza(string nome, string descrizione, string image, decimal prezzo)
        {
            Nome = nome;
            Descrizione = descrizione;
            Image = image;
            Prezzo = prezzo;
        }

        public Pizza()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        [StringLength(30, ErrorMessage = "Il nome non può avere più di 30 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        [StringLength(100, ErrorMessage = "La descrizione non può avere più di 100 caratteri")]
        [MoreWordValidation]
        public string Descrizione { get; set; }
        [Url(ErrorMessage = "Il campo immagine deve essere un Link")]
        [Required(ErrorMessage = "Il campo immagine è obbligatorio")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Il campo prezzo è obbligatorio")]
        [Range(1, 99, ErrorMessage = "Il prezzo deve essere fra 1 e 99 Euro")]
        public decimal Prezzo { get; set; }

        public int? CategoryId { get; set; }   
        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }
    }
}
