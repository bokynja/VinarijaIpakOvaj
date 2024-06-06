using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace Vinarija
{
    public partial class Proizvod : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        public Proizvod()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtNazivProizvoda.Focus();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiKategorije = @"Select kategorijaID, naziv_kategorije from Kategorija_proizvoda";
                DataTable dtKategorije = new DataTable();
                SqlDataAdapter daKategorije = new SqlDataAdapter(vratiKategorije, konekcija);
                daKategorije.Fill(dtKategorije);
                cbKategorija.ItemsSource = dtKategorije.DefaultView;
                dtKategorije.Dispose();
                daKategorije.Dispose();
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Proizvod (naziv, kategorijaID, cena) VALUES (@naziv, @kategorijaID, @cena)", konekcija);
                cmd.Parameters.AddWithValue("@naziv", txtNazivProizvoda.Text);
                cmd.Parameters.AddWithValue("@kategorijaID", cbKategorija.SelectedValue);
                cmd.Parameters.AddWithValue("@cena", txtCena.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Proizvod uspešno sačuvan!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom čuvanja proizvoda: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
