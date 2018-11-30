using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Generics
{
    /// <summary>
    /// Nachbaue der List<T>-Klasse
    /// </summary>
    public class MyList<ArrayType> : IEnumerable<ArrayType>
    {
        private ArrayType[] _internesArray;
        private int _anzahlElemente = 0;

        public MyList()
        {
            _internesArray = new ArrayType[0];
        }

        public void Add(ArrayType neuesItem)
        {
            ArrayType[] neuesArray = new ArrayType[_anzahlElemente + 1];
            if (_anzahlElemente > 0)
                Array.Copy(_internesArray, neuesArray, _anzahlElemente);

            neuesArray[_anzahlElemente] = neuesItem;
            _anzahlElemente++;


            _internesArray = neuesArray;

            //Warten/Forcieren auf Garbage-Collection
            GC.Collect(0);
        }

        //Kurzform von 
        //public int Count
        //{
        //    get => _internesArray.Length;
        //}
        public int Count => _internesArray.Length;

       

        public bool Remove(ArrayType zuEntfernendesItem)
        {
            if (!_internesArray.Contains(zuEntfernendesItem))
            {
                return false;
            }
            ArrayType[] neuesArray = new ArrayType[_anzahlElemente - 1];
            int indexNeu = 0;
            for (int i = 0; i < _internesArray.Length; i++)
            {
                if (_internesArray[i].Equals(zuEntfernendesItem))
                {

                }
                else
                {
                    neuesArray[indexNeu] = _internesArray[i];
                    indexNeu++;
                }
            }
            _internesArray = neuesArray;
            return true;
        }

        #region Implementierung von IEnumerable<ArrayType>---------------
        public IEnumerator<ArrayType> GetEnumerator()
        {
            return (IEnumerator<ArrayType>)GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //foreach (var item in _internesArray)
            //{
            //    yield return item;
            //}
            return _internesArray.GetEnumerator();
        }
        #endregion

        //Indexer
        public ArrayType this[int index]
        {
            get
            {
                if (index >= _internesArray.Length)
                {
                    throw new Exception("Index ist nicht vorhanden");
                }
                return _internesArray[index];
            }
            set
            {
                if (index >= _internesArray.Length)
                {
                    throw new Exception("Index ist nicht vorhanden");
                }
                _internesArray[index] = value;
            }
        }
    }
}
