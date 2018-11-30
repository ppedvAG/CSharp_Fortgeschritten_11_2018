using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Generische_Methoden
{
    public static class Helper
    {
        //Methode, die 2 Variablen vertauscht
        public static void Tausche(ref int var1, ref int var2)
        {
            int oldVar1 = var1;
            var1 = var2;
            var2 = oldVar1;
        }

        public static void TauscheOhneRef(string var1, string var2)
        {
            string oldVar1 = var1;
            var1 = var2;
            var2 = oldVar1;
        }

        public static void TauscheGeneric<T>(ref T var1, ref T var2)
        {
            T oldVar1 = var1;
            var1 = var2;
            var2 = oldVar1;
        }

        //Erweiterungsmethode
        public static int Quersumme(this int zahl)
        {
            //Verbesserung: Modulo-basiert berechnen

            string zahlAlsString = zahl.ToString();
            int summe = 0;
            foreach (var item in zahlAlsString)
            {
                summe += (int)item;
            }
            return summe;
        }

        public static void Tausche<T>(this ref T var1, ref T var2) where T : struct
        {
            T oldVar1 = var1;
            var1 = var2;
            var2 = oldVar1;
        }

        public static void AddValueToList<TKey, TValue, TListType>(this Dictionary<TKey, TListType> dict, TKey key, TValue value) where TListType : IList<TValue>,new()
        {

            if (!dict.ContainsKey(key) || dict[key] == null)
            {
                dict[key] = new TListType();
            }

            if (!dict[key].Contains(value))
            {
                dict[key].Add(value);
            }
            else
                throw new Exception("Eintrag schon vorhanden!");
        }

    }
}
