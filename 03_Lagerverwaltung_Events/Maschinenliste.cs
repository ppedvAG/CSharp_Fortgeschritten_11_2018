using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Lagerverwaltung_Events
{
    public class Maschinenlager : BindingList<Maschine>
    {
        public int Limit { get; set; }

        public bool NurEinExemplarProTyp { get; set; } = true;


        //Generischer Eventhandler

        //Delegate-Variable
        private EventHandler<Maschine> _lagerVoll;
        //Properties
        public event EventHandler<Maschine> LagerVoll
        {
            add { _lagerVoll += value; }
            remove { _lagerVoll -= value; }
        }

        public event EventHandler DoppelteMaschine;

        public Maschinenlager(int maxElements = 10)
        {
            Limit = maxElements;
        }

        public void ImportiereAndereListe(IEnumerable<Maschine> liste)
        {
            base.Clear();
            liste.ToList().ForEach(m => base.Add(m));
        }


        public new void Add(Maschine maschine)
        {
            if (base.Count >= Limit)
            {
                //Event auslösen
                if (_lagerVoll != null)
                {
                    _lagerVoll.Invoke(this, maschine);
                }
                return;
            }
            foreach (var item in this)
            {
                if (item == maschine)
                {
                    DoppelteMaschine?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

            base.Add(maschine);
        }
    }
}