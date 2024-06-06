using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarija.Models
{
    public class Narudzbina
    {
        [Key]
        public int NarudzbinaID { get; set; }

        public DateTime DatumNarudzbine { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        [StringLength(30)]
        public string AdresaIsporuke { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikID { get; set; }

        [ForeignKey("Zaposleni")]
        public int ZaposleniID { get; set; }

        [ForeignKey("Proizvod")]
        public int ProizvodID { get; set; }

        public Korisnik Korisnik { get; set; }
        public Zaposleni Zaposleni { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
