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

        MemoryCard _pierwsza = null; // <<<<<<<<<<<<<<<<< Lekcja 3
        MemoryCard _drugi = null; // <<<<<<<<<<<<<<<<< Lekcja 3

        public MemoryForm()
        {
            InitializeComponent();

            _settings = new GameSettings();

            UstawKontrolki();

            GenerujKarty();

            timerCzasPodgladu.Start();

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

                    b.Click += BtnClicked; // <<<<<<<<<<<<<<<<< Lekcja 3

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

        //----------------------------------------------------------------
        // <<<<<<<<<<<<<<<<< Lekcja 3
        //  Obsługa czasu podglądu kart i ich ukrycia
        //  Test - timerCzasPodgladu.Start(); w public MemoryForm()
        //----------------------------------------------------------------
        private void timerCzasPodgladu_Tick(object sender, EventArgs e)
        {
            _settings.CzasPodgladu--;

            lblStartInfo.Text = $"Początek gry za {_settings.CzasPodgladu}";

            if (_settings.CzasPodgladu <= 0)
            {
                lblStartInfo.Visible = false;

                foreach (Control konrolka in panelKart.Controls)
                {
                    MemoryCard card = (MemoryCard)konrolka;
                    card.Zakryj();
                }

                timerCzasPodgladu.Stop();

                timerCzasGry.Start();

            }
        }
        //----------------------------------------------------------------
        // <<<<<<<<<<<<<<<<< Lekcja 3
        //  Odkrywanie kart
        //----------------------------------------------------------------
        private void BtnClicked(object sender, EventArgs e)
        {
            MemoryCard btn = (MemoryCard)sender;

            if (_pierwsza == null)
            {
                _pierwsza = btn;
                _pierwsza.Odkryj();
            }
            else
            {
                _drugi = btn;
                _drugi.Odkryj();

                if ((_pierwsza.Id == _drugi.Id))
                {
                    _settings.AktualnePunkty++;

                    lblPunktyWartosc.Text = _settings.AktualnePunkty.ToString();

                    _pierwsza = null;
                    _drugi = null;

                    panelKart.Enabled = true;

                }

                else
                {
                    timerZakrywacz.Start();
                }

            }
        }
        //----------------------------------------------------------------
        // <<<<<<<<<<<<<<<<< Lekcja 3
        //Obsługa zakończenia gry + restartowanie
        //----------------------------------------------------------------
        private void timerZakrywacz_Tick(object sender, EventArgs e)
        {
            _pierwsza.Zakryj();
            _drugi.Zakryj();

            _pierwsza = null;
            _drugi = null;

            panelKart.Enabled = true;

            timerZakrywacz.Stop();
        }

        //----------------------------------------------------------------
        // <<<<<<<<<<<<<<<<< Lekcja 3
        //----------------------------------------------------------------

        private void timerCzasGry_Tick(object sender, EventArgs e)
        {
            _settings.CzasGry--;
            lblCzasWartosc.Text = _settings.CzasGry.ToString();

            if (_settings.CzasGry <= 0 || _settings.AktualnePunkty == _settings.MaxPunkty)
            {
                timerCzasGry.Stop();
                timerZakrywacz.Stop();

                DialogResult yesNo = MessageBox.Show($"Zdobyte punkty:{ _settings.AktualnePunkty}. Grasz ponownie?", "Koniec Gry", MessageBoxButtons.YesNo);

                if (yesNo == DialogResult.Yes)
                {

                    _settings.UstawStartowe();

                    GenerujKarty();
                    UstawKontrolki();

                    panelKart.Enabled = true;
                    _pierwsza = null;
                    _drugi = null;

                    timerCzasPodgladu.Start();
                }
                else
                {
                    Application.Exit();
                }

            }

        }
        //----------------------------------------------------------------
        //----------------------------------------------------------------






        //----------------------------------------------------------------
    }
}
