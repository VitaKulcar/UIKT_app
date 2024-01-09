using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    public class Delavec
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

        [Required(ErrorMessage = "Vnos v polje DATUM ZAPOSLITVE je obvezen.")]
        [RegularExpression(@"^[A-Za-zČčŠšŽž ]+$", ErrorMessage = "Vnos v polje DATUM ZAPOSLITVE mora biti datum.")]
        public DateOnly DatumZaposlitve { get; set; }

        [Required(ErrorMessage = "Vnos v polje STROKOVNI IZPIT je obvezen.")]
        public bool StrokovniIzpit { get; set; }

        [Required(ErrorMessage = "Vnos v polje PEDAGOŠKI IZPIT je obvezen.")]
        public bool PedagoskiIzpit { get; set; }

        public List<Vloga>? Vloge { get; set; }

        [NotMapped]
        public string StrokovniIzpitString => StrokovniIzpit ? "DA" : "NE";
        [NotMapped]
        public string PedagoskiIzpitString => PedagoskiIzpit ? "DA" : "NE";
    }
}
