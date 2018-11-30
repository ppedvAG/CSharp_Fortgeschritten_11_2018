using BuchInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleBooksBibliotheken
{
    public class DBSpeicher : IBuchSpeicher
    {

        FavoritenContext _context = new FavoritenContext();

        public List<IBuch> Laden()
        {
            _context.Favoriten.Load();

            List<IBuch> bücher = new List<IBuch>();
            foreach (var item in _context.Favoriten)
            {
                bücher.Add(item);
            }
            return bücher;
        }

        public void Speichern(List<IBuch> bücher)
        {
            
            if (bücher.Count > _context.Favoriten.Local.Count)
            {
                IBuch neuesterEintrag = bücher.Last();
                _context.Favoriten.AddOrUpdate((Buch)neuesterEintrag);
                _context.SaveChanges();

            }
            else if (bücher.Count < _context.Favoriten.Local.Count)
            {
                if (bücher.Count > 0)
                {

                    for (int i = 0; i < bücher.Count; i++)
                    {
                        Buch buch = _context.Favoriten.Local[i];

                        if (bücher[i].ISBN != _context.Favoriten.Local[i].ISBN)
                        {
                            _context.Favoriten.Remove(_context.Favoriten.Local[i]);
                            _context.SaveChanges();
                            break;
                        }
                    }
                }
                else
                {
                    _context.Favoriten.Remove(_context.Favoriten.Local[0]);
                    _context.SaveChanges();
                }

            }
        }
    }
}
