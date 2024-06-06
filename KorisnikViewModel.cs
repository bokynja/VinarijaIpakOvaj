using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data.SqlClient;
using System;

namespace Vinarija.ViewModels
{
    public class KorisnikViewModel : INotifyPropertyChanged
    {
        private string connectionString = "Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;";

        public ObservableCollection<Korisnik> Korisnici { get; set; }

        private Korisnik selectedKorisnik;
        public Korisnik SelectedKorisnik
        {
            get { return selectedKorisnik; }
            set
            {
                selectedKorisnik = value;
                OnPropertyChanged();
            }
        }

        public ICommand DodajCommand { get; }
        public ICommand IzmeniCommand { get; }
        public ICommand ObrisiCommand { get; }

        public KorisnikViewModel()
        {
            Korisnici = new ObservableCollection<Korisnik>();
            LoadKorisnici();

            DodajCommand = new RelayCommand(DodajKorisnika);
            IzmeniCommand = new RelayCommand(IzmeniKorisnika, CanModifyKorisnik);
            ObrisiCommand = new RelayCommand(ObrisiKorisnika, CanModifyKorisnik);
        }

        private void LoadKorisnici()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Korisnik", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Korisnici.Add(new Korisnik
                    {
                        KorisnikID = (int)reader["korisnikID"],
                        Ime = reader["ime"].ToString(),
                        Prezime = reader["prezime"].ToString(),
                        Email = reader["email"].ToString()
                    });
                }
            }
        }

        private void DodajKorisnika(object parameter)
        {
            Korisnik newKorisnik = new Korisnik(); // Parametar može biti proslijeđen iz pogleda
            if (newKorisnik != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Korisnik (ime, prezime, email) VALUES (@ime, @prezime, @Email); SELECT SCOPE_IDENTITY();",
                        connection);
                    command.Parameters.AddWithValue("@ime", newKorisnik.Ime);
                    command.Parameters.AddWithValue("@prezime", newKorisnik.Prezime);
                    command.Parameters.AddWithValue("@Email", newKorisnik.Email);

                    newKorisnik.KorisnikID = Convert.ToInt32(command.ExecuteScalar());
                    Korisnici.Add(newKorisnik);
                }
            }
        }

        private void IzmeniKorisnika(object parameter)
        {
            Korisnik updatedKorisnik = SelectedKorisnik; // Parametar može biti proslijeđen iz pogleda
            if (updatedKorisnik != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "UPDATE Korisnik SET ime = @ime, prezime = @prezime, email = @Email WHERE korisnikID = @korisnikID",
                        connection);
                    command.Parameters.AddWithValue("@ime", updatedKorisnik.Ime);
                    command.Parameters.AddWithValue("@prezime", updatedKorisnik.Prezime);
                    command.Parameters.AddWithValue("@Email", updatedKorisnik.Email);
                    command.Parameters.AddWithValue("@korisnikID", updatedKorisnik.KorisnikID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void ObrisiKorisnika(object parameter)
        {
            Korisnik korisnikToDelete = SelectedKorisnik; // Parametar može biti proslijeđen iz pogleda
            if (korisnikToDelete != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Korisnik WHERE korisnikID = @korisnikID", connection);
                    command.Parameters.AddWithValue("@korisnikID", korisnikToDelete.KorisnikID);
                    command.ExecuteNonQuery();
                    Korisnici.Remove(korisnikToDelete);
                }
            }
        }

        private bool CanModifyKorisnik(object parameter)
        {
            return SelectedKorisnik != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
