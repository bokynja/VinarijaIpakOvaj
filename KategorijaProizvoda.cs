using System.ComponentModel.DataAnnotations;

namespace Vinarija.Models
{
    public class KategorijaProizvoda
    {
        [Key]
        public int KategorijaID { get; set; }

        [Required]
        [StringLength(30)]
        public string NazivKategorije { get; set; }
    }
}
