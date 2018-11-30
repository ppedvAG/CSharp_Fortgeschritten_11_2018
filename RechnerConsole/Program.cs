using RechnerBibliothek;
using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerConsole
{
    class Program
    {
 
        static void Main(string[] args)
        {
            //Bootstrapping
            IRechner rechner = new RechnerZuordner(new Addition(), new Subtraktion(), new Division());
            IParser parser = new Parser();

            //GUI
            Console.WriteLine("Geben Sie die Aufgabe ein: ");
            string aufgabe = Console.ReadLine();

            //Parsing
            IGeparsteAufgabe geparsteAufgabe = parser.Parse(aufgabe);

            //Berechnung
            double ergebnis = rechner.Berechne(geparsteAufgabe);

            //GUI
            Console.WriteLine($"Ergebnis:  {ergebnis}");

            Console.WriteLine(double.IsPositiveInfinity(ergebnis));

            Console.ReadKey();
        }
    }

    #region Mock-Klassen
    public class MockRechner : IRechner
    {
        public double Berechne(IGeparsteAufgabe aufgabe)
        {
            return 20;
        }
    }

    public class MockParser : IParser
    {
        public IGeparsteAufgabe Parse(string eingabe)
        {
            return new MockGeparsteAufgabe();
        }
    }

    public class MockGeparsteAufgabe : IGeparsteAufgabe
    {
      
        public double Zahl1
        {
            get
            {
                return 12;
            }
        }

        public double Zahl2 => 10;

        public char Operator => '-';
    }
    #endregion
}
