using System.Windows;

namespace Vinarija
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenZaposleniWindow(object sender, RoutedEventArgs e)
        {
            ZaposleniWindow zaposleniWindow = new ZaposleniWindow();
            zaposleniWindow.Show();
        }

        private void OpenAdminWindow(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }

        private void OpenKorisnikWindow(object sender, RoutedEventArgs e)
        {
            Korisnik korisnikWindow = new Korisnik();
            korisnikWindow.Show();
        }

        private void OpenKategorijaProizvodaWindow(object sender, RoutedEventArgs e)
        {
            KategorijaProizvoda kategorijaProizvodaWindow = new KategorijaProizvoda();
            kategorijaProizvodaWindow.Show();
        }

        private void OpenProizvodWindow(object sender, RoutedEventArgs e)
        {
            Proizvod proizvodWindow = new Proizvod();
            proizvodWindow.Show();
        }

        private void OpenNarudzbinaWindow(object sender, RoutedEventArgs e)
        {
            Narudzbina narudzbinaWindow = new Narudzbina();
            narudzbinaWindow.Show();
        }

        private void OpenRezervacijaTureWindow(object sender, RoutedEventArgs e)
        {
            RezervacijaTure rezervacijaTureWindow = new RezervacijaTure();
            rezervacijaTureWindow.Show();
        }

        private void OpenOcenaProizvodaWindow(object sender, RoutedEventArgs e)
        {
            OcenaProizvodaWindow ocenaProizvodaWindow = new OcenaProizvodaWindow();
            ocenaProizvodaWindow.Show();
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
