using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Vinarija
{
    public partial class AdminWindow : Window
    {
        SqlConnection konekcija = new SqlConnection("Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;");
        bool azuriraj = false;
        DataRowView pomocniRed;

        public AdminWindow()
        {
            InitializeComponent();
            txtIme.Focus();
        }

        public AdminWindow(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            if (azuriraj)
            {
                txtIme.Text = pomocniRed["ime"].ToString();
                txtPrezime.Text = pomocniRed["prezime"].ToString();
                txtJmbg.Text = pomocniRed["jmbg"].ToString();
                txtTelefon.Text = pomocniRed["telefon"].ToString();
                txtAdresa.Text = pomocniRed["adresa"].ToString();
                dpDatumRodj.SelectedDate = (DateTime)pomocniRed["datum_rodj"];
            }
        }

        private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIme.Text) || string.IsNullOrEmpty(txtPrezime.Text) || string.IsNullOrEmpty(txtJmbg.Text) || dpDatumRodj.SelectedDate == null)
            {
                MessageBox.Show("Sva polja moraju biti popunjena.");
                return;
            }

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);
                cmd.Parameters.AddWithValue("@prezime", txtPrezime.Text);
                cmd.Parameters.AddWithValue("@jmbg", txtJmbg.Text);
                cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                cmd.Parameters.AddWithValue("@adresa", txtAdresa.Text);
                cmd.Parameters.AddWithValue("@datum_rodj", dpDatumRodj.SelectedDate);

                if (azuriraj)
                {
                    cmd.Parameters.AddWithValue("@id", pomocniRed["adminID"]);
                    cmd.CommandText = "UPDATE Admin SET ime=@ime, prezime=@prezime, jmbg=@jmbg, telefon=@telefon, adresa=@adresa, datum_rodj=@datum_rodj WHERE adminID=@id";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO Admin (ime, prezime, jmbg, telefon, adresa, datum_rodj) VALUES (@ime, @prezime, @jmbg, @telefon, @adresa, @datum_rodj)";
                }

                cmd.ExecuteNonQuery();
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unos određenih vrednosti nije validan! " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnPrikazi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Admin WHERE ime=@ime", konekcija);
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtPrezime.Text = reader["prezime"].ToString();
                    txtJmbg.Text = reader["jmbg"].ToString();
                    txtTelefon.Text = reader["telefon"].ToString();
                    txtAdresa.Text = reader["adresa"].ToString();
                    dpDatumRodj.SelectedDate = (DateTime)reader["datum_rodj"];
                }
                else
                {
                    MessageBox.Show("Admin sa unetim imenom ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom prikazivanja admina: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Admin SET ime=@ime, prezime=@prezime, jmbg=@jmbg, telefon=@telefon, adresa=@adresa, datum_rodj=@datum_rodj WHERE ime=@ime", konekcija);
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);
                cmd.Parameters.AddWithValue("@prezime", txtPrezime.Text);
                cmd.Parameters.AddWithValue("@jmbg", txtJmbg.Text);
                cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                cmd.Parameters.AddWithValue("@adresa", txtAdresa.Text);
                cmd.Parameters.AddWithValue("@datum_rodj", dpDatumRodj.SelectedDate);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Admin uspešno izmenjen!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Admin sa unetim imenom ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom izmene admina: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Admin WHERE ime=@ime", konekcija);
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Admin uspešno obrisan!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Admin sa unetim imenom ne postoji.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Greška prilikom brisanja admina: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }
    }
}
