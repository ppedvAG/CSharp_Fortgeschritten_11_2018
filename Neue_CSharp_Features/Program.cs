using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neue_CSharp_Features
{
    class Program
    {
        static void Main(string[] args)
        {
            bool r = MachWas(out _);
            bool r2 = MachWas(out int z);
            Console.WriteLine(z);

            CaseMitRanges(12);

            var erg = MethodeMitMehrerenRückgabewerten();
            if(erg.hatGeklappt)
                Console.WriteLine(erg.ergebnis);

            (int e, bool ag) meinTuple = MethodeMitMehrerenRückgabewerten();
            if(meinTuple.ag)
            {
                Console.WriteLine(meinTuple.e);
            }

            StarteProgramm();

            bool variable = true;
            Random random = new Random();
            ReadonlyRefMethode(variable, random);

            FurchtbareMethode(angabe: "dsad", 
                              zahl: 2, 
                              flag1: true, 
                              flag2: false, limit: 1);

            Console.ReadKey();
        }


        private static void FurchtbareMethode(int zahl, string angabe, int limit, bool flag1, bool flag2)
        {

        }
        private static void ReadonlyRefMethode(in bool flag, Random random )
        {
            if(flag)
                Console.WriteLine("True");
            else
                Console.WriteLine("False");
        }

        private static void StarteProgramm()
        {
            int x = 3;
            int y = 5;

            Console.WriteLine(Berechne(x, y));


            int Berechne(int z1, int z2)
            {
                return z1 + z2;
            }

            int z = 1_000_000;

            int[] array = new int[] { 2, 5, 3, 2 };

            int keineReferenz = GetIndexVariable(array, 2);
            keineReferenz = array[2];
            
            ref int referenz = ref GetIndexVariable(array, 2);
            ref int referenz2 = ref array[2];
            
            int zahl = 5;
            ref int refZahl = ref zahl;
            
            refZahl = 6;
            Console.WriteLine($"Zahl  {zahl}");

            referenz2 = 10;

            Console.WriteLine($"Array nach Änderung: {array[2]}");

            referenz = 10;


            string leererString = null;

            MacheWasMitRandom(leererString);

        }

        private static void MacheWasMitRandom(string wort)
        {
            wort = wort ?? "Standard";
            wort = (wort == null) ? "Standard" : wort;
            wort = wort ?? throw new Exception("darf nicht null sein");

            if (wort == null)
                wort = "Standard";
        }

        private static ref int GetIndexVariable(int[] zahlen, int index)
        {
            return ref zahlen[index];
        }

        private static (int ergebnis, bool hatGeklappt) MethodeMitMehrerenRückgabewerten()
        {
            return (2, true);
        }



        private static void CaseMitRanges(object @object)
        {
            if(@object is int zahl && zahl > 0 && zahl < 10)
            {
                
            }

            switch (@object)
            {
                case int z when (z > 0 && z < 10):
                    Console.WriteLine();
                    break;
                case int z when (z >= 10 && z < 100):

                    break;
                case int z when (z >= 100):

                    break;
                default:
                    break;
            }
        }

        public static bool MachWas(out int zahl)
        {
            zahl = 5;
            return true;
        }
    }
}

    
