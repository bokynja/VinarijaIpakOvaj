using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace Vinarija
{
    public partial class RezervacijaTure : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        public RezervacijaTure()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            dpDatumRezervacije.Focus();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                DataTable dtKorisnici = new DataTable();
                SqlDataAdapter daKorisnici = new SqlDataAdapter("Select korisnikID, ime from Korisnik", konekcija);
                daKorisnici.Fill(dtKorisnici);
                cbKorisnik.ItemsSource = dtKorisnici.DefaultView;

                DataTable dtZaposleni = new DataTable();
                SqlDataAdapter daZaposleni = new SqlDataAdapter("Select zaposleniID, ime from Zaposleni", konekcija);
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Rezervacija_ture (datum_rezervacije, korisnikID, zaposleniID) VALUES (@datum_rezervacije, @korisnikID, @zaposleniID)", konekcija);
                cmd.Parameters.AddWithValue("@datum_rezervacije", dpDatumRezervacije.SelectedDate);
                cmd.Parameters.AddWithValue("@korisnikID", cbKorisnik.SelectedValue);
                cmd.Parameters.AddWithValue("@zaposleniID", cbZaposleni.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Rezervacija ture uspešno sačuvana!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom čuvanja rezervacije: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}