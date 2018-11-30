using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerBibliothek
{
    public class Rechner : IRechner
    {
        public virtual double Berechne(IGeparsteAufgabe aufgabe)
        {
            double zahl1 = aufgabe.Zahl1;
            double zahl2 = aufgabe.Zahl2;
            char symbol = aufgabe.Operator;

            switch (symbol)
            {
                case '+':
                    return zahl1 + zahl2;
                case '-':
                    return zahl1 - zahl2;
                default:
                    throw new FormatException("Unbekanntes Rechensymbol");
            }
        }
    }
}
