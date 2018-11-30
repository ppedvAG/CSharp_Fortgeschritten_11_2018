using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerBibliothek
{
    public class Formel : IGeparsteAufgabe
    {

        public double Zahl1 { get; private set; }

        public double Zahl2 { get; private set; }

        public char Operator { get; private set; }

        public Formel(double zahl1, double zahl2, char @operator)
        {
            Zahl1 = zahl1;
            Zahl2 = zahl2;
            Operator = @operator;
        }
    }
}
