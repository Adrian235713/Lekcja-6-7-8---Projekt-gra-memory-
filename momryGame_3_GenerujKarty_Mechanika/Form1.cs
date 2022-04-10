using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace momryGame_1_Podstawowe_kontrolki
{
    public partial class MemoryForm : Form
    {
        private GameSettings _settings;

        //MemoryCard _pierwsza = null;
        //MemoryCard _drugi = null;

        public MemoryForm()
        {
            InitializeComponent();

            _settings = new GameSettings();

            UstawKontrolki();

            GenerujKarty();
        }

        //----------------------------------------------------------------
        private void UstawKontrolki()
        {
            panelKart.Width = _settings.Bok * _settings.Kolumny;
            panelKart.Height = _settings.Bok * _settings.Wiersze;

            Width = panelKart.Width + 100;
            Height = panelKart.Height + 150;

            lblStartInfo.Text = $"Początek gry za {_settings.CzasPodgladu}.";
            lblPunktyWartosc.Text = _settings.AktualnePunkty.ToString();
            lblCzasWartosc.Text = _settings.CzasGry.ToString();

            lblStartInfo.Visible = true;
        }
        //----------------------------------------------------------------
        private void GenerujKarty()
        {

        //Brawo ja popraw sciezki w GameStings.cs ..... 
        //public string PlikLogo = $@"{AppDomain.CurrentDomain.BaseDirectory}\obrazki\logo.jpg";
        //public string FolderObrazki = $@"{AppDomain.CurrentDomain.BaseDirectory}\obrazki\karty";

        string[] memories = Directory.GetFiles(_settings.FolderObrazki);
            _settings.MaxPunkty = memories.Length;

            List<MemoryCard> buttons = new List<MemoryCard>();

            foreach(string img in memories)
            {
                Guid id = Guid.NewGuid();

                MemoryCard b1 = new MemoryCard(id, _settings.PlikLogo, img);

                buttons.Add(b1);

                MemoryCard b2 = new MemoryCard(id, _settings.PlikLogo, img);

                buttons.Add(b2);
            }

            Random random = new Random();

            panelKart.Controls.Clear();

            for(int x = 0; x < _settings.Kolumny; x++)
            {
                for(int y = 0; y < _settings.Wiersze; y++)
                {
                    int inddex = random.Next(0, buttons.Count);

                    MemoryCard b = buttons[inddex];

                    int margines = 2;

                    b.Location = new Point((x * _settings.Bok) + (margines * x), (y *_settings.Bok) + (margines * y));

                    b.Width = _settings.Bok;
                    b.Height = _settings.Bok;

                    b.Odkryj();

                    panelKart.Controls.Add(b);

                    buttons.Remove(b);
                }
            }

        }


    }
}
