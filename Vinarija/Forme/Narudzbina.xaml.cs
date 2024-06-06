using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Vinarija
{
    public partial class Narudzbina : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();

        public Narudzbina()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            dpDatumNarudzbine.Focus();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                // Popuni korisnike
                DataTable dtKorisnici = new DataTable();
                SqlDataAdapter daKorisnici = new SqlDataAdapter("Select korisnikID, ime from Korisnik", konekcija);
                daKorisnici.Fill(dtKorisnici);
                cbKorisnik.ItemsSource = dtKorisnici.DefaultView;

                // Popuni zaposlene
                DataTable dtZaposleni = new DataTable();
                SqlDataAdapter daZaposleni = new SqlDataAdapter("Select zaposleniID, ime from Zaposleni", konekcija);
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;

                // Popuni proizvode
                DataTable dtProizvodi = new DataTable();
                SqlDataAdapter daProizvodi = new SqlDataAdapter("Select proizvodID, naziv from Proizvod", konekcija);
                daProizvodi.Fill(dtProizvodi);
                cbProizvod.ItemsSource = dtProizvodi.DefaultView;
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Narudzbina (datum_narudzbine, status, adresa_isporuke, korisnikID, zaposleniID, proizvodID) VALUES (@datum_narudzbine, @status, @adresa_isporuke, @korisnikID, @zaposleniID, @proizvodID)", konekcija);
                cmd.Parameters.AddWithValue("@datum_narudzbine", dpDatumNarudzbine.SelectedDate);
                cmd.Parameters.AddWithValue("@status", ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString());
                cmd.Parameters.AddWithValue("@adresa_isporuke", txtAdresaIsporuke.Text);
                cmd.Parameters.AddWithValue("@korisnikID", cbKorisnik.SelectedValue);
                cmd.Parameters.AddWithValue("@zaposleniID", cbZaposleni.SelectedValue);
                cmd.Parameters.AddWithValue("@proizvodID", cbProizvod.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Narudžbina uspešno sačuvana!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom čuvanja narudžbine: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
