using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerBibliothek
{
    public class Division : IRechenoperation
    {
        public char Symbol => '/';

        public double Berechne(IGeparsteAufgabe formel)
        {
            if(formel.Zahl2 == 0)
            {
                throw new DivideByZeroException();
            }
            return formel.Zahl1 / formel.Zahl2;
        }
    }
}