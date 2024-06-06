using System.ComponentModel.DataAnnotations;

namespace Vinarija.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }

        [Required]
        [StringLength(10)]
        public string Ime { get; set; }

        [Required]
        [StringLength(15)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string JMBG { get; set; }

        public long Telefon { get; set; }

        [StringLength(25)]
        public string Adresa { get; set; }
    }
}
