using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Generische_Methoden
{
    class Program
    {
        static void Main(string[] args)
        { 
            Dictionary<string, List<string>> einwohnerliste = new Dictionary<string, List<string>>();
           
            einwohnerliste.AddValueToList("Münster", "Anja");
            einwohnerliste.AddValueToList("Münster", "Matthias");

            Console.WriteLine(einwohnerliste["Münster"][0]);
            Console.WriteLine(einwohnerliste["Münster"][1]);

            int zahl1 = 5;
            int zahl2 = 10;
            Helper.TauscheGeneric(ref zahl1, ref zahl2);
            Console.WriteLine($"zahl1 {zahl1}");
            Console.WriteLine($"zahl2 {zahl2}");

            string name1 = "Alex";
            string name2 = "Torsten";
            Helper.TauscheOhneRef(name1, name2);
            Console.WriteLine($"name1 {name1}");
            Console.WriteLine($"name2 {name2}");

            double z1 = 5.5;
            double z2 = 10.2;
            z1.Tausche(ref z2);


            Helper.TauscheGeneric(ref name1, ref name2);


            int quersumme = (52525).Quersumme();
            Console.WriteLine(quersumme);

            Console.ReadKey();
        }
    }
}
