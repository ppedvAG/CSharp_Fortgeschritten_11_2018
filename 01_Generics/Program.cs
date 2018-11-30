using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _01_Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            //BeispieleFÜrGenerics();
            //MotivationFürGenerics();
            //MyListBeispiel();
            DictionaryBeispiel();
            EinsZuNDictionaryTest();
            Console.ReadKey();
        }

        private static void EinsZuNDictionaryTest()
        {
            EinsZuN_Dictionary<string, string> einwohnerliste = new EinsZuN_Dictionary<string, string>();
            einwohnerliste.Add("Münster", "Markus");
            einwohnerliste.Add("Münster", "Anna");
            einwohnerliste.Add("Münster", null);

            Console.WriteLine("Einwohner in Münster");
            foreach (var item in einwohnerliste["Münster"])
            {
                Console.WriteLine(item);
            }
        }

        #region DictionaryBeispiel
        private static void DictionaryBeispiel()
        {
            Dictionary<string, List<string>> einwohnerliste = new Dictionary<string, List<string>>();
            einwohnerliste.Add("Schwarzenbek", new List<string>() { "Matthias", "Markus", "Joerg" });
            einwohnerliste["Schwarzenbek"].Add("Hans");
            Dictionary<int, int> zahlAufZahl = new Dictionary<int, int>();
            
        }
        #endregion

        #region MyListBeispiel
        private static void MyListBeispiel()
        {
            MyList<int> zahlenListe = new MyList<int>();
            zahlenListe.Add(5);
            zahlenListe.Add(10);
            zahlenListe.Add(12);

            int summe = 0;
            foreach (var item in zahlenListe)
            {
                summe += item;
            }

            int zahl1 = zahlenListe[1];
            zahlenListe[1] = 4;

            int entferneZahl = 10;

            zahlenListe.Remove(entferneZahl);


        }
        #endregion

        #region MotivationFürGenerics
        private static void MotivationFürGenerics()
        {
            ArrayList namensliste = new ArrayList();
            //Boxing: Wertetyp -> object
            namensliste.Add(8);
            namensliste.Add(12);
            namensliste.Add(20);

            int summe = 0;
            foreach (var item in namensliste)
            {
                //Unboxing: Casting von object -> Wertetyp
                summe += (int)item;
            }

            int z1 = 5;
            //Bei Wertetypen (structs) werden die Werte kopiert bei einer Zuweisung
            int z1Copy = z1;

            List<string> namensliste2 = new List<string>();
            namensliste.Add("Torsten");
            namensliste.Add("Alex");
            List<string> namenslisteCopy = namensliste2;
            //Bei Referenztypen (class) werde die Speicheradressen zugewiesen
            namenslisteCopy[0] = "Kevin";

            //Jeder Struct ist ein Wertetyp
            DateTime zeitpunkt;
            TimeSpan zeitraum;
        }
        #endregion

        #region BeispieleFÜrGenerics
        private static void BeispieleFÜrGenerics()
        {
            List<string> namensliste = new List<string>();
            namensliste.Add("Torsten");
            namensliste.Add("Alex");
            

            Dictionary<string, string> namenStädteDict = new Dictionary<string, string>();
            namenStädteDict.Add("Angela Merkel", "Berlin");

            Tuple<int, bool> ergebnis = new Tuple<int, bool>(20, true);

            XmlSerializer serializer = new XmlSerializer(typeof(int));
            //JsonConvert.Serialize<int>(30);

            //Generische Delegates
            Action<int> methodeMitEinemParameterUndKeinemRückgabewert;
            Func<int, bool> methodeMitEinemParameterUndRückgabewertBool;

            //Generische EventHandler
            EventHandler<int> eventHandlermitIntegerAlsEventArgs;

            //Entitiy Framework
            //DbSet<Person> personenTabelle;
        }
        #endregion
    }
}
