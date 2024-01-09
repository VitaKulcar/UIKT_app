using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class Vloga
    {
        [Key]
        public int Id { get; set; }

        public Guid? Identifikator { get; set; }

        public List<Delavec>? Delavci { get; set; }

        public Vlagatelj? Vlagatelj { get; set; }

        public bool? KoncanaVloga {  get; set; }

        public Vloga() { }

        public Vloga(List<Delavec> delavci, Vlagatelj vlagatelj)
        {
            Identifikator = Guid.NewGuid();
            Delavci = delavci;
            Vlagatelj = vlagatelj;
        }
    }
}
