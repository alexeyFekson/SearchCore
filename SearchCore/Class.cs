using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchCore
{
    public class MinHeap
    {
        private int[] _values;
        private int idx = 0;
        private int size;
        public int[] Values { get => _values; set { _values = value; } }


        public MinHeap(int size)
        {
            this._values = new int[size];
            this.size = size;
        }
        public bool insert(int num)
        {

            if (idx == size - 1) return false;
            _values[idx++] = num;
            return Hepify(idx - 1);
        }

        private bool Hepify(int idx)
        {
            int ptr = idx;
            while (ptr > 0)
            {
                // if num is smaller then parent , swap them 
                if (_values[ptr] < _values[(ptr - 1) / 2])
                {
                    int tmp = _values[ptr];
                    _values[ptr] = _values[(ptr - 1) / 2];
                    _values[ptr] = tmp;
                }
                ptr = (ptr - 1) / 2;
            }
            return true;

        }

    }
   
}
