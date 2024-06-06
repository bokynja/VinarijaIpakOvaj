using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data.SqlClient;
using System;

namespace Vinarija.ViewModels
{
    public class KategorijaProizvodaViewModel : INotifyPropertyChanged
    {
        private string connectionString = "Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;";

        public ObservableCollection<KategorijaProizvoda> Kategorije { get; set; }

        private KategorijaProizvoda selectedKategorija;
        public KategorijaProizvoda SelectedKategorija
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

        public KategorijaProizvodaViewModel()
        {
            Kategorije = new ObservableCollection<KategorijaProizvoda>();
            LoadKategorije();

            DodajCommand = new RelayCommand(DodajKategoriju);
            IzmeniCommand = new RelayCommand(IzmeniKategoriju, CanModifyKategorija);
            ObrisiCommand = new RelayCommand(ObrisiKategoriju, CanModifyKategorija);
        }

        private void LoadKategorije()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM KategorijaProizvoda", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Kategorije.Add(new KategorijaProizvoda
                    {
                        KategorijaProizvodaID = (int)reader["kategorijaProizvodaID"],
                        Naziv = reader["naziv"].ToString()
                    });
                }
            }
        }

        private void DodajKategoriju(object parameter)
        {
            KategorijaProizvoda newKategorija = new KategorijaProizvoda(); // Parametar može biti proslijeđen iz pogleda
            if (newKategorija != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO KategorijaProizvoda (naziv) VALUES (@naziv); SELECT SCOPE_IDENTITY();",
                        connection);
                    command.Parameters.AddWithValue("@naziv", newKategorija.Naziv);

                    newKategorija.KategorijaProizvodaID = Convert.ToInt32(command.ExecuteScalar());
                    Kategorije.Add(newKategorija);
                }
            }
        }

        private void IzmeniKategoriju(object parameter)
        {
            KategorijaProizvoda updatedKategorija = SelectedKategorija; // Parametar može biti proslijeđen iz pogleda
            if (updatedKategorija != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "UPDATE KategorijaProizvoda SET naziv = @naziv WHERE kategorijaProizvodaID = @kategorijaProizvodaID",
                        connection);
                    command.Parameters.AddWithValue("@naziv", updatedKategorija.Naziv);
                    command.Parameters.AddWithValue("@kategorijaProizvodaID", updatedKategorija.KategorijaProizvodaID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void ObrisiKategoriju(object parameter)
        {
            KategorijaProizvoda kategorijaToDelete = SelectedKategorija; // Parametar može biti proslijeđen iz pogleda
            if (kategorijaToDelete != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM KategorijaProizvoda WHERE kategorijaProizvodaID = @kategorijaProizvodaID", connection);
                    command.Parameters.AddWithValue("@kategorijaProizvodaID", kategorijaToDelete.KategorijaProizvodaID);
                    command.ExecuteNonQuery();
                    Kategorije.Remove(kategorijaToDelete);
                }
            }
        }

        private bool CanModifyKategorija(object parameter)
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
