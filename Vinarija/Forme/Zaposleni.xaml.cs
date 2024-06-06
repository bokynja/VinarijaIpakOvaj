using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Vinarija
{
    public partial class ZaposleniWindow : Window
    {
        SqlConnection konekcija = new SqlConnection("Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;");
        bool azuriraj = false;
        DataRowView pomocniRed;

        public ZaposleniWindow()
        {
            InitializeComponent();
            txtIme.Focus();
        }

        public ZaposleniWindow(bool azuriraj, DataRowView pomocniRed)
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
                    cmd.Parameters.AddWithValue("@id", pomocniRed["zaposleniID"]);
                    cmd.CommandText = "UPDATE Zaposleni SET ime=@ime, prezime=@prezime, jmbg=@jmbg, telefon=@telefon, adresa=@adresa, datum_rodj=@datum_rodj WHERE zaposleniID=@id";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO Zaposleni (ime, prezime, jmbg, telefon, adresa, datum_rodj) VALUES (@ime, @prezime, @jmbg, @telefon, @adresa, @datum_rodj)";
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
    }
}
