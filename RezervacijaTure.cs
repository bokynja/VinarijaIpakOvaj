using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarija.Models
{
    public class RezervacijaTure
    {
        [Key]
        public int RezervacijaID { get; set; }

        public DateTime DatumRezervacije { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikID { get; set; }

        [ForeignKey("Zaposleni")]
        public int ZaposleniID { get; set; }

        public Korisnik Korisnik { get; set; }
        public Zaposleni Zaposleni { get; set; }
    }
}
