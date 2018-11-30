using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuchInterfaces;
using GoogleBooksBibliotheken;

namespace GoogleBooksClient
{
    public partial class Form1 : Form
    {
        //Bootstrapping
        IWebservice webservice = new GoogleBooksAPI();
        IFavoritenManager favoritenManager = new FavoritenManager();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = false;
            dataGridView1.CellClick += DataGridView1_CellClick;
            //es ist wichtig das DataError-Event zu behandeln, da dass Programm bei Auftritt eine 
            //Fehlers ansonsten abstürzt
            dataGridView1.DataError += DataGridView1_DataError;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"Problem beim Datensatz in Zeile: {e.RowIndex} und Spalte: {e.ColumnIndex}");
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.IndexOf(buttonColumn))
            {
                string link = _bücher[e.RowIndex].Vorschaulink;
                Process.Start(link);
            }
        }

        DataGridViewButtonColumn buttonColumn;
        BindingList<IBuch> _bücher;
        int checkBoxColumnIndex;

        private void zeigeBücherImDataGridAn(List<IBuch> bücher, bool syncList = false)
        {
            if (_bücher != null)
                _bücher.ListChanged -= Bücher_ListChanged;

            this._bücher = new BindingList<IBuch>(bücher);
            _bücher.ListChanged += Bücher_ListChanged;
            favoritenManager.SyncList = syncList ?  _bücher : null;
            dataGridView1.DataSource = _bücher;

            //Überflüssige Spalte entfernen (Vorschaulink)
            var column = dataGridView1.Columns[nameof(IBuch.Vorschaulink)];
            dataGridView1.Columns.Remove(column);

            checkBoxColumnIndex = dataGridView1.Columns.Count - 1;

            if (buttonColumn != null)
                return;

            //Button-Spalte hinzufügen
            buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Vorschau";
            buttonColumn.Text = "Vorschau";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(buttonColumn);
        }

        private async void button_suche_click(object sender, EventArgs e)
        {
            string suchbegriff = textBoxSuchbegriff.Text;

            zeigeBücherImDataGridAn(await webservice.SucheBücher(suchbegriff));
        }

        private void Bücher_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == nameof(IBuch.IstFavorit)) {
                if (e.NewIndex >= _bücher.Count)
                    return;
                IBuch buch = _bücher[e.NewIndex];
                //False -> True
                if (buch.IstFavorit)
                {
                    favoritenManager.FügeAlsFavoritHinzu(buch);
                }
                //True -> False
                else
                {
                    favoritenManager.EntferneAlsFavorit(buch);
                }
            }
        }

        private void textBoxSuchbegriff_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button_suche_click(this, EventArgs.Empty);
            }
        }

        private void button_zeige_favoriten(object sender, EventArgs e)
        {
            zeigeBücherImDataGridAn(favoritenManager.Favoriten, true);
        }
    }
}
