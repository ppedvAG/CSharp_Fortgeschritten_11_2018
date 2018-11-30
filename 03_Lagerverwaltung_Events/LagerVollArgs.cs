using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Lagerverwaltung_Events
{
    class LagerVollArgs : EventArgs
    {
        public Maschine Maschine { get; set; }

        public LagerVollArgs(Maschine maschine)
        {
            Maschine = maschine;
        }
    }
}
