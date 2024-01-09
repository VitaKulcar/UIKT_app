using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    public class Vlagatelj : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vnos v polje IME je obvezen.")]
        [RegularExpression(@"^[A-Za-zČčŠšŽž ]+$", ErrorMessage = "Vnos v polje IME lahko vsebuje samo črke.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Vnos v polje PRIIMEK je obvezen.")]
        [RegularExpression(@"^[A-Za-zČčŠšŽž ]+$", ErrorMessage = "Vnos v polje PRIIMEK lahko vsebuje samo črke.")]
        public string Priimek { get; set; }

        [Required(ErrorMessage = "Vnos v polje IME ZAVODA je obvezen.")]
        [RegularExpression(@"^[A-Za-zČčŠšŽž ]+$", ErrorMessage = "Vnos v polje IME ZAVODA lahko vsebuje samo črke.")]
        public string ImeZavoda { get; set; }

        public List<Vloga>? Vloge { get; set; }

        public Vlagatelj() { }

        public Vlagatelj(string imeZavoda, string ime, string priimek)
        {
            ImeZavoda = imeZavoda;
            Ime = ime;
            Priimek = priimek;
        }
    }
}
