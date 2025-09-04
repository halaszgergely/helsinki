 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace helsinki
{
    public partial class MainWindow : Window
    {
        struct lista
        {
            public int helyezes;
            public int letszam;
            public string sportag;
            public string versenyszam;
        }
        List<lista> adatok = new List<lista>();
        public MainWindow()
        {
            InitializeComponent();
            beolvas();
            helyezesek_kiir();
            osszsport();
            olimpia_pont();
            uszastorna();
            kijavit();
            maxletszam();
        }

        private void maxletszam()
        {
            var letszam = 0;
            foreach (var item in adatok)
            {
                if (item.letszam > letszam)
                {
                    if (letszam < item.letszam)
                    {
                        letszam = item.letszam;
                    }
                }
            }
            foreach (var item in adatok)
            {
                if (item.letszam == letszam)
                {
                    tb2.Text = $"Helyezés: {item.helyezes}\n Sportág: {item.sportag}\n Versenyszám: {item.versenyszam}\n Sportolók létszáma: {item.letszam}";
                }
            }
        }

        private void kijavit()
        {
            using (var sw = new StreamWriter("helsinki2.txt"))
            {
                foreach(var item in adatok)
                {
                    if (item.sportag == "kajakkenu")
                    {
                        sw.WriteLine($"{item.helyezes} kajak-kenu {item.sportag} {item.versenyszam}");
                    }
                    else
                    {
                        sw.WriteLine($"{item.helyezes} {item.letszam} {item.sportag} {item.versenyszam}");
                    }
                }
            }
        }

        private void uszastorna()
        {
            var uszas = 0;
            var torna = 0;
            foreach (var item in adatok) 
            {
                if (item.sportag == "uszas")
                {
                    uszas++;
                }
                else
                {
                    torna++;
                }
            }
            if(uszas > torna)
            {
                lb3.Content = "Az úszás sportágban szereztek több érmet.";
            }
            else if (uszas < torna)
            {
                lb3.Content = "A torna sportágban szereztek több érmet.";
            }
            else
            {
                lb3.Content = "A két sportágban egyenlő számú érmet szereztek.";
            }
        }

        private void olimpia_pont()
        {
            int pont = 0;
            foreach (var item in adatok)
            {
                switch (item.helyezes)
                {
                    case 1:
                        pont += 7;
                        break;
                    case 2:
                        pont += 5;
                        break;
                    case 3:
                        pont += 4;
                        break;
                    case 4:
                        pont += 3;
                        break;
                    case 5:
                        pont += 2;
                        break;
                    case 6:
                        pont++;
                        break;
                }
            }
            lb2.Content += pont.ToString();
        }

        private void osszsport()
        {
            int i = 0;
            foreach (var item in adatok)
            {
                i++;
            }
            lb1.Content += i.ToString();
        }

        private void helyezesek_kiir()
        {
            var arany = 0;
            var ezust = 0;
            var bronz = 0;
            foreach ( var item in adatok)
            {
                switch(item.helyezes){
                    case 1:
                        arany++;
                        break;
                    case 2:
                        ezust++;
                        break;
                    case 3:
                        bronz++;
                        break;
                }
                tb1.Text = $"arany \t {arany} \n ezüst \t {ezust} \n bronz \t {bronz} \n Összesen: \t {arany+ezust+bronz}";
            }
        }
        private void beolvas()
        {
            using (var sr = new StreamReader("helsinki.txt"))
            {
                while (!sr.EndOfStream) { 
                    var temp = sr.ReadLine().Split(' ');
                    if (adatok.Count < 200)
                    {
                        var rekord = new lista();
                        rekord.helyezes = int.Parse(temp[0]);
                        rekord.letszam = int.Parse(temp[1]);
                        rekord.sportag = temp[2];
                        rekord.versenyszam = temp[3];
                        adatok.Add(rekord);
                    }
                }
            }
        }
    }
}
