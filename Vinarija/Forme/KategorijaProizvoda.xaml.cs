using System;
using System.Data.SqlClient;
using System.Windows;


namespace Vinarija
{
    public partial class KategorijaProizvoda : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();

        public KategorijaProizvoda()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNazivKategorije.Focus();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Kategorija_proizvoda (naziv_kategorije) VALUES (@naziv_kategorije)", konekcija);
                cmd.Parameters.AddWithValue("@naziv_kategorije", txtNazivKategorije.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Kategorija proizvoda uspešno sačuvana!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom čuvanja kategorije proizvoda: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
