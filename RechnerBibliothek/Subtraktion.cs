using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerBibliothek
{
    public class Subtraktion : IRechenoperation
    {
        public char Symbol => '-';

        public double Berechne(IGeparsteAufgabe formel)
        {
            return formel.Zahl1 - formel.Zahl2;
        }
    }
}
