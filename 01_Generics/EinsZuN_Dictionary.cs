using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Generics
{
    public class EinsZuN_Dictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>
    {
        public void Add(TKey key, TValue value)
        {

            //if(!base.ContainsKey(key))
            //{
            //    List<TValue> neueListe = new List<TValue>();
            //    neueListe.Add(value);
            //    base.Add(key, neueListe);
            //}
            //else
            //{
            //    List<TValue> vorhandeneListe = base[key];
            //    if(vorhandeneListe.Contains(value))
            //    {
            //        throw new Exception("Eintrag schon vorhanden!");
            //    }
            //    vorhandeneListe.Add(value);
            //}

            //Jakobsweg
            if (!base.ContainsKey(key) || base[key] == null)
            {
                base[key] = new List<TValue>();
            }

            if (!base[key].Contains(value))
            {
                base[key].Add(value);
            }
            else
                throw new Exception("Eintrag schon vorhanden!");
        }
    }
}
