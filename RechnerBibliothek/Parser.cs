using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerBibliothek
{
    public class Parser : IParser
    {
        public IGeparsteAufgabe Parse(string eingabe)
        {
            //Nullable char
            char? rechensymbol = null;

            foreach (var zeichen in eingabe)
            {
                if(!char.IsNumber(zeichen) && zeichen != ',' && zeichen != '.')
                {
                    rechensymbol = zeichen;
                    break;
                }
            }

            if(rechensymbol == null)
            {
                throw new Exception("Kein Rechensymbol gefunden");
            }

            var teile = eingabe.Split((char)rechensymbol);
            double zahl1 = double.Parse(teile[0]);
            double zahl2 = double.Parse(teile[1]);

            return new Formel(zahl1, zahl2, (char)rechensymbol);
        }
    }
}
