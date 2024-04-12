using System.Diagnostics;
using System.Net;
using System.Windows;

namespace VersionChecker
{
/// <summary>
/// Ez a CS lekéri a program verzióját és a hozzátartozó Github linket.
/// Először meg kell adni a VerzioLink_Lekeres funkciónak a Program_ID-t ami meg van adva a PasteBin-es linken. Pl.: VerzioLink_Lekeres(Teszt)
/// Ha megadtuk akkor a publikus int-eket és stringeket lehet használni. Csak akkor használjuk ezeket, ha előtte meg lett hívva a funkció!
/// 
/// Példa kód:
/// 
/// using VersionChecker;
/// VersionCheck vs = new VersionCheck();
/// vs.VerzioLink_Lekeres("Gabi_fenykep_atnevezo");
/// Console.WriteLine(vs.FoVerzio.ToString() + vs.MellekVerzio.ToString() + vs.Program_Link);
/// 
/// </summary>
    
    class VersionCheck
    {
        public int FoVerzio, MellekVerzio = 0;
        public string Program_Link="";
        public void VerzioLink_Lekeres(string Program_ID) 
        {
            string PastebinLink = "https://pastebin.com/raw/QrNE4Qt4";

            WebClient web = new WebClient();
            string FullString = web.DownloadString(PastebinLink);

            int ID_Index = FullString.IndexOf(Program_ID);

            string ID_Sor = FullString.Substring(ID_Index);
            
            string[] StringDarabok = ID_Sor.Split(';');

            FoVerzio = int.Parse(StringDarabok[1]);
            MellekVerzio = int.Parse(StringDarabok[2]);
            Program_Link = StringDarabok[3];
        }
        #region Verzió Ellenőrzése
        public void VerzioEll(int foverzio, int mellekverzio)
        {
            VerzioLink_Lekeres("Gabi_fenykep_atnevezo");
            if (FoVerzio != foverzio || MellekVerzio != mellekverzio)
            {
                var Result = MessageBox.Show("Egy új frissítés elérhető! A "+FoVerzio+"."+MellekVerzio+" verzió elérhető. Neked jelenleg a "+foverzio+"."+mellekverzio+ " van.\nSzeretnéd az új verziót letölteni?","Frissítés",MessageBoxButton.YesNo,MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = Program_Link,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                    Environment.Exit(0);
                }
            }
        }
        #endregion

    }
}

