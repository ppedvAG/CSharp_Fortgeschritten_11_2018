using BuchInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GoogleBooksBibliotheken
{
    public class FavoritenManager : IFavoritenManager
    {

        IBuchSpeicher _buchSpeicher = new DBSpeicher();
        //Für Speicherung in Datei stattdessen folgende Klasse initialisieren:
        //IBuchSpeicher _buchSpeicher = new XMLSpeicher();

        private List<IBuch> _favoriten = new List<IBuch>();
        public List<IBuch> Favoriten => new List<IBuch>(_favoriten);

        /// <summary>
        /// Liste im Frontend, die mit der Favoritenliste synchronisiert werden soll
        /// </summary>
        public IList<IBuch> SyncList { get; set; }

        public FavoritenManager()
        {
            _favoriten = _buchSpeicher.Laden();
            var treffer = _favoriten.FindAll(b => !b.IstFavorit);

            if(treffer.Count > 0)
            {
                var b = treffer[0];
            }
                
            _favoriten.ForEach(b => b.IstFavorit = true);

        }

        public void EntferneAlsFavorit(IBuch buch)
        {
            IBuch zuLöschendesBuch = _favoriten.FirstOrDefault(b => b.ISBN == buch.ISBN);

            if (zuLöschendesBuch == null)
                return;

            _favoriten.Remove(zuLöschendesBuch);

            SyncList?.Remove(buch);
            _buchSpeicher.Speichern(_favoriten);
        }

        public void FügeAlsFavoritHinzu(IBuch buch)
        {
            if (_favoriten.Any(b => b.ISBN == buch.ISBN))
            {
                return;
            }

            _favoriten.Add(buch);
            SyncList?.Add(buch);
            _buchSpeicher.Speichern(_favoriten);
        }
    }
}
