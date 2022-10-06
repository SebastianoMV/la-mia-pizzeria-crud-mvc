using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_post
{
    public class MoreWordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldValue = (string) value;
            List<string> stringa = new();

            if (fieldValue != null)
            {
                fieldValue = fieldValue.Trim();
                foreach (string parola in fieldValue.Split(' '))
                {
                    stringa.Add(parola);
                }

                if (stringa.Count < 5)
                {
                    return new ValidationResult("Il campo descrizione deve contenere almeno cinque parole");
                }
            }

            

            return ValidationResult.Success;
        }
    }
}
