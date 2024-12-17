using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KonyvtarHttpClient.DTOs;
using KonyvtarHttpClient.Models;
using KonyvtarHttpClient.Services;

namespace KonyvtarHttpClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri(" http://localhost:5000"),
        };
        private static List<KonyvtarakDTO> konyvtarak = new List<KonyvtarakDTO>();

        public async MainWindow()
        {
            InitializeComponent();
            konyvtarak = await KonyvtarServices.GetAll(sharedClient);
            Task.Delay(1000).Wait();
            dgrAdatok.ItemsSource = konyvtarak;
        }

        private void dgrAdatSelChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrAdatok.SelectedItem != null)
            {
                tbxId.Text = (dgrAdatok.SelectedItem as KonyvtarakDTO).Id.ToString();
                tbxKonyvtarNev.Text = (dgrAdatok.SelectedItem as KonyvtarakDTO).KonyvtarNev;
                tbxIrsz.Text = (dgrAdatok.SelectedItem as KonyvtarakDTO).Irsz.ToString();
                tbxTelepnev.Text = (dgrAdatok.SelectedItem as KonyvtarakDTO).Telepnev;
                tbxCim.Text = (dgrAdatok.SelectedItem as KonyvtarakDTO).Cim;
                tbxMegyenev.Text = (dgrAdatok.SelectedItem as KonyvtarakDTO).Megyenev;
            }
        }

        private async void UjKonyvtar(object sender, RoutedEventArgs e)
        {
            Konyvtarak ujKonyvtar = new Konyvtarak()
            {
                Id = 0,
                KonyvtarNev = tbxKonyvtarNev.Text,
                Irsz = int.Parse(tbxIrsz.Text),
                Cim = tbxCim.Text,
                IrszNavigation = null
            };
            await KonyvtarServices.Post(sharedClient, ujKonyvtar);
            Task.Delay(1000).Wait();
            konyvtarak = await KonyvtarServices.GetAll(sharedClient);
            Task.Delay(1000).Wait();
            dgrAdatok.Items.Refresh();
            dgrAdatok.SelectedIndex = 0;

        }

        private async void TorolKonyvtar(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan törlöd {tbxId.Text} azonosítójú könyvtárat?", "Törlés", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                await KonyvtarServices.Delete(sharedClient, int.Parse(tbxId.Text));
                Task.Delay(1000).Wait();
            }
            konyvtarak = await KonyvtarServices.GetAll(sharedClient);
            Task.Delay(1000).Wait();
            dgrAdatok.Items.Refresh();
            dgrAdatok.SelectedIndex = 0;
        }

        private async void ModositKonyvtar(object sender, RoutedEventArgs e)
        {
            Konyvtarak ujKonyvtar = new Konyvtarak()
            {
                Id = int.Parse(tbxId.Text),
                KonyvtarNev = tbxKonyvtarNev.Text,
                Irsz = int.Parse(tbxIrsz.Text),
                Cim = tbxCim.Text,
                IrszNavigation = null
            };
            await KonyvtarServices.Put(sharedClient, ujKonyvtar);
            Task.Delay(1000).Wait();
            KonyvtarServices.GetAll(sharedClient, konyvtarak);
            Task.Delay(1000).Wait();
            dgrAdatok.Items.Refresh();
            dgrAdatok.SelectedIndex = 0;
        }

    }
}