using System;
using System.Collections.Generic;

namespace string_generator
{
    class StringGenerator
    {
        #region Változók
        private List<string> Karakter_lista = new List<string>();

        #region Karakterek
        private string[] nagybetű = new string[23] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "X", "Y", "Z" };
        private string[] kisbetű = new string[23] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "x", "y", "z" };
        private string[] szám = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private string[] egyedi_karakterek = new string[25] { "!", ".", ",", "/", "$", "?", ">", "<", "{", "}", "[", "]", "(", ")", "'", "+", "-", "*", "=", "%", "§", "&", "@", "_", ";" };
        #endregion Karakterek

        private Random r = new Random();
        private int generáló_hossza;
        private int karakter_index;
        private int lista_méret = 0;

        public string Eredmény;

        public bool Nagybetűk = true;
        public bool Kisbetűk = true;
        public bool Számok = true;
        public bool Egyedi_karakterek = true;
        #endregion Változók

        /// <summary>
        /// Ezzel a metódussal legenerálható egy egyedi string. Az <c>Eredmény</c> string változó meghívásával megkapjuk a generált eredményünket. 
        /// <para>Több beállítása is van amiket ki-be lehet kapcsolni:</para>
        /// <list type="number">
        /// <item>Nagybetűk</item>
        /// <item>Kisbetűk</item>
        /// <item>Számok</item>
        /// <item>Egyedi karakterek</item>
        /// </list>
        /// </summary>

        public void Generálás(int Generáló_hossza)
        {
            if (Generáló_hossza <= 0)
            {
                throw new ArgumentException("Hiba! Nem vehet fel a generátor hossza '0' vagy annál kisebb értéket!");
            }
            if (Nagybetűk == false && Kisbetűk == false && Számok == false && Egyedi_karakterek == false)
            {
                throw new ArgumentException("Hiba! Nem lehet minden tulajdonság kikapcsolva!");
            }

            generáló_hossza = Generáló_hossza;

            if (Nagybetűk == true)
            {
                lista_méret += 22;
                Karakter_lista.AddRange(nagybetű);
            }
            if (Kisbetűk == true)
            {
                lista_méret += 22;
                Karakter_lista.AddRange(kisbetű);
            }
            if (Számok == true)
            {
                lista_méret += 9;
                Karakter_lista.AddRange(szám);
            }
            if (Egyedi_karakterek == true)
            {
                lista_méret += 24;
                Karakter_lista.AddRange(egyedi_karakterek);
            }            

            for (int i = 0; i < generáló_hossza; i++)
            {
                karakter_index = r.Next(0, lista_méret);
                Eredmény += Karakter_lista[karakter_index];
            }
        }
    }
}
