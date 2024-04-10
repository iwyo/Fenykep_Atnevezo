using Microsoft.Win32;
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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using string_generator;


namespace Gabi_fenykep_atnevezo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TB_Tallozas_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog();
            bool? ertek = ofd.ShowDialog();
            if (ertek == true)
            {
                string mappaneve = ofd.FolderName;
                TB_Tallozas.Text = mappaneve;
                Kep_Szam.Content = Kepfajlok(mappaneve)+" db";
            }                       
        }

        private void Atnevezes(string mappa_utvonal)
        {
            bool siker = false;
                // A mappa összes fájlán végigmegyünk
                foreach (string regiFajlEleresiUt in Directory.GetFiles(mappa_utvonal))
                {
                    if (HaKep(regiFajlEleresiUt))
                    {
                        // Csak a fájlnév részt különválasztjuk
                        string fajlNev = System.IO.Path.GetFileNameWithoutExtension(regiFajlEleresiUt);

                        // Ellenőrizzük, hogy a fájlnév hossza nagyobb-e, mint 8
                        if (fajlNev.Length > 8)
                        {
                            // Az új fájlnév csak az első 8 karakter lesz
                            string ujFajlNev = fajlNev.Substring(0, 8);

                            // Az új fájl teljes elérési útja
                            string ujFajlEleresiUt = System.IO.Path.Combine(mappa_utvonal, ujFajlNev + System.IO.Path.GetExtension(regiFajlEleresiUt));

                            // Átnevezi a fájlt
                            File.Move(regiFajlEleresiUt, ujFajlEleresiUt);
                            siker = true;
                        }
                    }
                    
                }
            if (siker == true)
            {
                MessageBox.Show("Sikerült átnevezni a mappában lévő " + Kepfajlok(mappa_utvonal) + "db" + " képet!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nincs átnevezendő kép!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Tallozas.Text != "")
            {
                Atnevezes(TB_Tallozas.Text.ToString());
            }
        }

        #region DEBUG GENERALAS
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            // Fájlok generálása és írása
            for (int i = 1; i <= 30; i++)
            {               
                StringGenerator SG = new StringGenerator();
                SG.Nagybetűk = false;
                SG.Kisbetűk = true;
                SG.Számok = true;
                SG.Egyedi_karakterek = false;
                SG.Generálás(random.Next(1,10));
                string filePath = @"F:\Programming\Teszt\" + $"DSC{random.Next(10000, 99999)}" + SG.Eredmény + ".png";
                File.WriteAllText(filePath, "");
            }

            MessageBox.Show($"A 30 fájl sikeresen generálva lett.", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region KÉP DARABSZÁM ELLENŐRZÉS
        private int Kepfajlok(string mappa_utvonal)
        {
            string[] fajlok = Directory.GetFiles(mappa_utvonal);
            string[] kepKiterjesztesek = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            int kepFajlokSzama = 0;

            foreach (string fajl in fajlok)
            {
                string kiterjesztes = System.IO.Path.GetExtension(fajl).ToLower();

                if (Array.Exists(kepKiterjesztesek, element => element == kiterjesztes))
                {
                    kepFajlokSzama++;
                }
            }
            return kepFajlokSzama;
        }
        #endregion

        #region FÁJL KITERJESZTÉS VIZSGÁLATA
        static bool HaKep(string fajlNeve)
        {
            string kiterjesztes = System.IO.Path.GetExtension(fajlNeve).ToLower();
            return kiterjesztes == ".jpg" || kiterjesztes == ".jpeg" || kiterjesztes == ".png" || kiterjesztes == ".gif" || kiterjesztes == ".bmp";
        }
        #endregion
    }
}