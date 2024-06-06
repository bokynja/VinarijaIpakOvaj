using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data.SqlClient;
using System;

namespace Vinarija.ViewModels
{
    public class KategorijaViewModel : INotifyPropertyChanged
    {
        private string connectionString = "Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;";

        public ObservableCollection<Kategorija> Kategorije { get; set; }

        private Kategorija selectedKategorija;
        public Kategorija SelectedKategorija
        {
            get { return selectedKategorija; }
            set
            {
                selectedKategorija = value;
                OnPropertyChanged();
            }
        }

        public ICommand DodajCommand { get; }
        public ICommand IzmeniCommand { get; }
        public ICommand ObrisiCommand { get; }

        public KategorijaViewModel()
        {
            Kategorije = new ObservableCollection<Kategorija>();
            LoadKategorije();

            DodajCommand = new RelayCommand(DodajKategoriju);
            IzmeniCommand = new RelayCommand(IzmeniKategoriju, CanModifyKategoriju);
            ObrisiCommand = new RelayCommand(ObrisiKategoriju, CanModifyKategoriju);
        }

        private void LoadKategorije()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Kategorija", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Kategorije.Add(new Kategorija
                    {
                        KategorijaID = (int)reader["kategorijaID"],
                        Naziv = reader["naziv"].ToString()
                    });
                }
            }
        }

        private void DodajKategoriju(object parameter)
        {
            Kategorija newKategorija = new Kategorija(); // Parametar može biti proslijeđen iz pogleda
            if (newKategorija != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Kategorija (naziv) VALUES (@naziv); SELECT SCOPE_IDENTITY();",
                        connection);
                    command.Parameters.AddWithValue("@naziv", newKategorija.Naziv);

                    newKategorija.KategorijaID = Convert.ToInt32(command.ExecuteScalar());
                    Kategorije.Add(newKategorija);
                }
            }
        }

        private void IzmeniKategoriju(object parameter)
        {
            Kategorija updatedKategorija = SelectedKategorija; // Parametar može biti proslijeđen iz pogleda
            if (updatedKategorija != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "UPDATE Kategorija SET naziv = @naziv WHERE kategorijaID = @kategorijaID",
                        connection);
                    command.Parameters.AddWithValue("@naziv", updatedKategorija.Naziv);
                    command.Parameters.AddWithValue("@kategorijaID", updatedKategorija.KategorijaID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void ObrisiKategoriju(object parameter)
        {
            Kategorija kategorijaToDelete = SelectedKategorija; // Parametar može biti proslijeđen iz pogleda
            if (kategorijaToDelete != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Kategorija WHERE kategorijaID = @kategorijaID", connection);
                    command.Parameters.AddWithValue("@kategorijaID", kategorijaToDelete.KategorijaID);
                    command.ExecuteNonQuery();
                    Kategorije.Remove(kategorijaToDelete);
                }
            }
        }

        private bool CanModifyKategoriju(object parameter)
        {
            return SelectedKategorija != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
