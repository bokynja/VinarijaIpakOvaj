using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data.SqlClient;
using System;

namespace Vinarija.ViewModels
{
    public class ProizvodViewModel : INotifyPropertyChanged
    {
        private string connectionString = "Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;";

        public ObservableCollection<Proizvod> Proizvodi { get; set; }

        private Proizvod selectedProizvod;
        public Proizvod SelectedProizvod
        {
            get { return selectedProizvod; }
            set
            {
                selectedProizvod = value;
                OnPropertyChanged();
            }
        }

        public ICommand DodajCommand { get; }
        public ICommand IzmeniCommand { get; }
        public ICommand ObrisiCommand { get; }

        public ProizvodViewModel()
        {
            Proizvodi = new ObservableCollection<Proizvod>();
            LoadProizvodi();

            DodajCommand = new RelayCommand(DodajProizvod);
            IzmeniCommand = new RelayCommand(IzmeniProizvod, CanModifyProizvod);
            ObrisiCommand = new RelayCommand(ObrisiProizvod, CanModifyProizvod);
        }

        private void LoadProizvodi()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Proizvod", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Proizvodi.Add(new Proizvod
                    {
                        ProizvodID = (int)reader["proizvodID"],
                        Naziv = reader["naziv"].ToString(),
                        KategorijaID = (int)reader["kategorijaID"],
                        Cena = (decimal)reader["cena"]
                    });
                }
            }
        }

        private void DodajProizvod(object parameter)
        {
            Proizvod newProizvod = new Proizvod(); // Parametar može biti proslijeđen iz pogleda
            if (newProizvod != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Proizvod (naziv, kategorijaID, cena) VALUES (@naziv, @kategorijaID, @cena); SELECT SCOPE_IDENTITY();",
                        connection);
                    command.Parameters.AddWithValue("@naziv", newProizvod.Naziv);
                    command.Parameters.AddWithValue("@kategorijaID", newProizvod.KategorijaID);
                    command.Parameters.AddWithValue("@cena", newProizvod.Cena);

                    newProizvod.ProizvodID = Convert.ToInt32(command.ExecuteScalar());
                    Proizvodi.Add(newProizvod);
                }
            }
        }

        private void IzmeniProizvod(object parameter)
        {
            Proizvod updatedProizvod = SelectedProizvod; // Parametar može biti proslijeđen iz pogleda
            if (updatedProizvod != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "UPDATE Proizvod SET naziv = @naziv, kategorijaID = @kategorijaID, cena = @cena WHERE proizvodID = @proizvodID",
                        connection);
                    command.Parameters.AddWithValue("@naziv", updatedProizvod.Naziv);
                    command.Parameters.AddWithValue("@kategorijaID", updatedProizvod.KategorijaID);
                    command.Parameters.AddWithValue("@cena", updatedProizvod.Cena);
                    command.Parameters.AddWithValue("@proizvodID", updatedProizvod.ProizvodID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void ObrisiProizvod(object parameter)
        {
            Proizvod proizvodToDelete = SelectedProizvod; // Parametar može biti proslijeđen iz pogleda
            if (proizvodToDelete != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Proizvod WHERE proizvodID = @proizvodID", connection);
                    command.Parameters.AddWithValue("@proizvodID", proizvodToDelete.ProizvodID);
                    command.ExecuteNonQuery();
                    Proizvodi.Remove(proizvodToDelete);
                }
            }
        }

        private bool CanModifyProizvod(object parameter)
        {
            return SelectedProizvod != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
    {
        // Inicijalizacija kolekcije proizvoda i dodavanje slika
        Proizvodi = new ObservableCollection<Proizvod>();

        // Dodajte slike
        Proizvodi.Add(new Proizvod { Naziv = "vino1.png", Slika = "C:\Users\Korisnik\Desktop\januar.februar\Eksploatacija\vino1.png", Cena = 100, Opis = "Opis vina 1" });
Proizvodi.Add(new Proizvod { Naziv = "vino3.png", Slika = "C:\\Users\\Korisnik\\Desktop\\januar.februar\\Eksploatacija\\vino3.png", Cena = 120, Opis = "Opis vina 2" });
        // Dodajte ostale proizvode...
    }
}
