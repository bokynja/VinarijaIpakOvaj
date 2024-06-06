using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarija.Models
{
    public class OcenaProizvoda
    {
        [Key]
        public int OcenaID { get; set; }

        [StringLength(100)]
        public string Opis { get; set; }

        public int Ocena { get; set; }

        [ForeignKey("Proizvod")]
        public int ProizvodID { get; set; }

        public Proizvod Proizvod { get; set; }
    }
}
