using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03_Lagerverwaltung_Events
{
    public partial class Form1 : Form
    {
        Maschinenlager Lager1 = new Maschinenlager(7)
        {
            new Maschine("FE50", true),
            new Maschine("FE100", true),
            new Maschine("FE75", false),
            new Maschine("FE55", true),
            new Maschine("FE35", false)
        };

        public Form1()
        {
            InitializeComponent();
            Lager1.LagerVoll += Lager1_LagerVoll;
            Lager1.DoppelteMaschine += Lager1_DoppelteMaschine;
            listBox1.DataSource = Lager1;
        }

        private void Lager1_DoppelteMaschine(object sender, EventArgs e)
        {
            MessageBox.Show("Die Maschine gibt es schon");
        }

        private void Lager1_LagerVoll(object lager, Maschine m)
        {
            MessageBox.Show($"{m} konnte nicht hinzugefügt werden\n" +
                $"Lager ist voll! Schnell verkaufen!");
            this.BackColor = Color.Red;
            //Email schicken, Lager abräumen...
        }

        private void button_erzeuge_maschine(object sender, EventArgs e)
        {
            Maschine neueMaschine = new Maschine(textBox1.Text, checkBox1.Checked);
            
            Lager1.Add(neueMaschine);
        }

        private void button_sortieren(object sender, EventArgs e)
        {
            //Sortierung: Ansteigend nach Typ
            var sortierteListe = Lager1.OrderByDescending(MachinenSortierkriterium).ToList();
            Lager1.ImportiereAndereListe(sortierteListe);
            listBox1.DataSource = Lager1;
        }

        public int MachinenSortierkriterium(Maschine maschine)
        {
            var v = maschine.Typ.Substring(2);
            return int.Parse(maschine.Typ.Substring(2));
        }

        private void button_filtern_click(object sender, EventArgs e)
        {
            //Waschbare Maschinen
            var gefilterteListe = Lager1.Where(FilterMethode).ToList();
            Lager1.ImportiereAndereListe(gefilterteListe);
            listBox1.DataSource = Lager1;


            //Filtering mit eigener Where-Methode
            MyWhere(Lager1.ToList(), FilterMethode);

            //Anonyme Methoden (Lambda)
            //Filterung und Sortierung in Kombination, anschließend jedes Element in Ergebnisliste in MessageBox ausgeben
            Lager1.Where(m => m.Waschbar).ToList().OrderBy(m => int.Parse(m.Typ.Substring(2))).ToList().ForEach(m =>
                {
                    MessageBox.Show(m.ToString());
                }
            );
        }

        /// <summary>
        /// Nachbau der Where-Methode von LINQ
        /// </summary>
        /// <typeparam name="T">Datentyp der zu filternden Objekte</typeparam>
        /// <param name="liste">zu filternde Liste</param>
        /// <param name="algo"></param>
        /// <returns>gefilterte Liste</returns>
        public List<T> MyWhere<T>(List<T> liste, Func<T, bool> algo)
        {
            List<T> gefilterteListe = new List<T>();
            foreach (var item in liste)
            {
                if (algo.Invoke(item) == true)
                    gefilterteListe.Add(item);
            }
            return gefilterteListe;
        }

        public bool FilterMethode(Maschine maschine)
        {
            //Nur waschbare Maschinen
            return maschine.Waschbar;
        }
    }
}
