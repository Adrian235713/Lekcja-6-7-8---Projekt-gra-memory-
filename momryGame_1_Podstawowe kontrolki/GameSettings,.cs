using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace momryGame_1_Podstawowe_kontrolki
{
    public class GameSettings
    {
        public int CzasGry;
        public int CzasPodgladu;
        public int MaxPunkty;
        public int Wiersze;
        public int Kolumny;
        public int Bok;
        public int AktualnePunkty;

        public string PlikLogo = $@"{AppDomain.CurrentDomain.BaseDirectory}\obrazki\logo.jpg";
        public string FolderObrazki = $@"{AppDomain.CurrentDomain.BaseDirectory}\obrazki\karty";

        public GameSettings()
        {
            UstawStartowe();
        }


        public void UstawStartowe()
        {
            CzasPodgladu = 5;
            CzasGry = 60;
            MaxPunkty = 0;
            Wiersze = 4;
            Kolumny = 6;
            Bok = 150;
            AktualnePunkty = 0;
        }

    }
}
