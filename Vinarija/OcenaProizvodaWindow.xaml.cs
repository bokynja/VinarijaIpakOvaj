using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Vinarija;


namespace Vinarija
{
    public partial class OcenaProizvoda : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        public OcenaProizvoda()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtOcena.Focus();
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Ocena_proizvoda (ocena, opis, korisnikID, proizvodID) VALUES (@ocena, @opis, @korisnikID, @proizvodID)", konekcija);
                cmd.Parameters.AddWithValue("@ocena", txtOcena.Text);
                cmd.Parameters.AddWithValue("@opis", txtOpis.Text);
                cmd.Parameters.AddWithValue("@korisnikID", cbKorisnik.SelectedValue);
                cmd.Parameters.AddWithValue("@proizvodID", cbProizvod.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Ocena proizvoda uspešno sačuvana!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom čuvanja ocene proizvoda: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
