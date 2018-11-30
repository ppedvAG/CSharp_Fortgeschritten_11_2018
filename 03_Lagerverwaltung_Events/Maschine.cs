using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Lagerverwaltung_Events
{
    public class Maschine
    {

        public static long AnzahlMaschinen = 50;

        public string Typ { get; set; }
        public long Nummer { get; set; }
        public bool Waschbar { get; set; }

        public Maschine(string typ, bool waschbar)
        {
            Typ = typ;

            Nummer = AnzahlMaschinen++;

            Waschbar = waschbar;
        }
        public override string ToString()
        {
            string waschString = Waschbar ? "ist waschbar" : "ist nicht waschbar";
            return $"{Typ} ({Nummer}) {waschString}";
        }

        public override bool Equals(object obj)
        {
            if(obj is Maschine maschine)
            {
                return this.Typ == maschine.Typ;
            }
            return false;

            //Jakobsweg: return obj is Maschine x && x.Typ == Typ; 
        }

        public static bool operator ==(Maschine m1, Maschine m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Maschine m1, Maschine m2)
        {
            return !m1.Equals(m2);
        }

    }
}

