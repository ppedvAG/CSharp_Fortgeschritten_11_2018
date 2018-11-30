using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RechnerContracts;

namespace RechnerBibliothek
{
    public class RechnerZuordner : Rechner
    {
        public List<IRechenoperation> Operationen { get; set; }

        public RechnerZuordner(params IRechenoperation[] operationen)
        {
            Operationen = operationen.ToList();
        }

        public override double Berechne(IGeparsteAufgabe aufgabe)
        {
            foreach (var rechenoperation in Operationen)
            {
                if(aufgabe.Operator == rechenoperation.Symbol)
                {
                    return rechenoperation.Berechne(aufgabe);
                }
            }
            throw new Exception("Unbekanntes Rechensymbol");
        }
    }
}
