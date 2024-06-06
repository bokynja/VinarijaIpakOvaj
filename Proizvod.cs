using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarija.Models
{
    public class Proizvod
    {
        [Key]
        public int ProizvodID { get; set; }

        [StringLength(70)]
        public string Naziv { get; set; }

        [ForeignKey("KategorijaProizvoda")]
        public int? KategorijaID { get; set; }

        public decimal? Cena { get; set; }

        public KategorijaProizvoda KategorijaProizvoda { get; set; }
    }
}
