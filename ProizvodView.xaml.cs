using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Vinarija.Views
{
    public partial class ProizvodView : Window
    {
        public ProizvodView()
        {
            InitializeComponent();
        }

        // Dodajte implementaciju ProizvodClicked metode ovde
        private void ProizvodClicked(object sender, MouseButtonEventArgs e)
        {
            // Dobijanje selektovanog proizvoda
            var selectedProizvod = (sender as Image).DataContext as Proizvod;

            // Prikazivanje informacija o proizvodu, na primer u MessageBox-u
            MessageBox.Show($"Naziv: {selectedProizvod.Naziv}\nCena: {selectedProizvod.Cena}\nOpis: {selectedProizvod.Opis}");
        }
    }
}
