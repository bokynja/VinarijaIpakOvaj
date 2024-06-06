using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data.SqlClient;

namespace Vinarija.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private string connectionString = "Server=BOJANA\\SQLEXPRESS;Database=Vinarija;Integrated Security=True;";

        public ObservableCollection<Admin> Admini { get; set; }

        private Admin selectedAdmin;
        public Admin SelectedAdmin
        {
            get { return selectedAdmin; }
            set
            {
                selectedAdmin = value;
                OnPropertyChanged();
            }
        }

        public ICommand DodajCommand { get; }
        public ICommand IzmeniCommand { get; }
        public ICommand ObrisiCommand { get; }

        public AdminViewModel()
        {
            Admini = new ObservableCollection<Admin>();
            LoadAdmini();

            DodajCommand = new RelayCommand(DodajAdmina);
            IzmeniCommand = new RelayCommand(IzmeniAdmina, CanModifyAdmin);
            ObrisiCommand = new RelayCommand(ObrisiAdmina, CanModifyAdmin);
        }

        private void LoadAdmini()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Admin", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Admini.Add(new Admin
                    {
                        AdminID = (int)reader["adminID"],
                        Ime = reader["ime"].ToString(),
                        Prezime = reader["prezime"].ToString(),
                        Email = reader["email"].ToString()
                    });
                }
            }
        }

        private void DodajAdmina(object parameter)


