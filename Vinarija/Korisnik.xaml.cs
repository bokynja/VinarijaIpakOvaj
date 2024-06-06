using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Vinarija
{
    public partial class Korisnik : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();

        public Korisnik()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
        }

        // SELECT operacija
        private void btnPrikazi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Korisnik WHERE jmbg=@jmbg", konekcija);
                cmd.Parameters.AddWithValue("@jmbg", txtJMBG.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Postavljanje vrednosti u TextBox kontrole
                    txtIme.Text = reader["ime"].ToString();
                    txtPrezime.Text = reader["prezime"].ToString();
                    txtTelefon.Text = reader["telefon"].ToString();
                    txtAdresa.Text = reader["adresa"].ToString();
                }
                else
                {
                    MessageBox.Show("Korisnik sa unetim JMBG-jem ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom prikazivanja korisnika: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        // INSERT operacija
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Korisnik (ime, prezime, jmbg, telefon, adresa) VALUES (@ime, @prezime, @jmbg, @telefon, @adresa)", konekcija);
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);
                cmd.Parameters.AddWithValue("@prezime", txtPrezime.Text);
                cmd.Parameters.AddWithValue("@jmbg", txtJMBG.Text);
                cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                cmd.Parameters.AddWithValue("@adresa", txtAdresa.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Korisnik uspešno dodat!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom čuvanja korisnika: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        // UPDATE operacija
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Korisnik SET ime=@ime, prezime=@prezime, telefon=@telefon, adresa=@adresa WHERE jmbg=@jmbg", konekcija);
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);
                cmd.Parameters.AddWithValue("@prezime", txtPrezime.Text);
                cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                cmd.Parameters.AddWithValue("@adresa", txtAdresa.Text);
                cmd.Parameters.AddWithValue("@jmbg", txtJMBG.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Korisnik uspešno izmenjen!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Korisnik sa unetim JMBG-jem ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom izmene korisnika: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        // DELETE operacija
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Korisnik WHERE jmbg=@jmbg", konekcija);
                cmd.Parameters.AddWithValue("@jmbg", txtJMBG.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Korisnik uspešno obrisan!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Korisnik sa unetim JMBG-jem ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom brisanja korisnika: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        

        private void btnPogledaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Korisnik WHERE jmbg=@jmbg", konekcija);
                cmd.Parameters.AddWithValue("@jmbg", txtJMBG.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Postavljanje vrednosti u TextBox kontrole
                    txtIme.Text = reader["ime"].ToString();
                    txtPrezime.Text = reader["prezime"].ToString();
                    txtTelefon.Text = reader["telefon"].ToString();
                    txtAdresa.Text = reader["adresa"].ToString();
                }
                else
                {
                    MessageBox.Show("Korisnik sa unetim JMBG-jem ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom prikazivanja korisnika: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        

    }
}
