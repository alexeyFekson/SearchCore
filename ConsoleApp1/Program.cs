using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public class MinHeap
        {
            private int idx = 0;
            private int size;
            public int[] Values { get; set; }


            public MinHeap(int size)
            {
                this.Values = new int[size];
                this.size = size;
            }
            public bool Insert(int num)
            {

                if (idx == size - 1) return false;
                Values[idx++] = num;
                return Hepify(idx - 1);
            }

            private bool Hepify(int idx)
            {
                int ptr = idx;
                while (ptr > 0)
                {
                    // if num is smaller then parent , swap them 
                    if (Values[ptr] < Values[(ptr - 1) / 2])
                    {
                        int tmp = Values[ptr];
                        Values[ptr] = Values[(ptr - 1) / 2];
                        Values[(ptr - 1) / 2] = tmp;
                    }
                    ptr = (ptr - 1) / 2;
                }
                return true;

            }

            private int Min()
            {
                if (idx == 0) return int.MaxValue;
                if (idx == 1)
                {
                    idx--;
                    return Values[0];
                }
                int res = Values[0];
                Values[0] = Values[idx - 1];
                idx--;
                HeapDown(0);
                return res;

            }


            private void HeapDown(int current)
            {
                //   if (current >= idx - 1) return;
                // has not left child
                int leftIdx = current * 2 + 1;
                int rightIdx = current * 2 + 2;
                if (leftIdx >idx - 1) return;
                
                // we have only left chile
                if (rightIdx > idx - 1)
                {
                    if (Values[current] > Values[leftIdx])
                    {
                        int tmp = Values[current];
                        Values[current] = Values[leftIdx];
                        Values[leftIdx] = tmp;
                        return;
                    }
                }

                if (Values[rightIdx] < Values[leftIdx] && Values[current] > Values[rightIdx])
                {
                    int tmp1 = Values[current];
                    Values[current] = Values[rightIdx];
                    Values[rightIdx] = tmp1;
                    HeapDown(rightIdx);
                }

                if (Values[current] < Math.Min(Values[leftIdx], Values[rightIdx])) return;
                int tmp2 = Values[current];
                Values[current] = Values[leftIdx];
                Values[leftIdx] = tmp2;
                HeapDown(leftIdx);

            }
            static void Main(string[] args)
            {
                MinHeap heap = new MinHeap(10);
                heap.Insert(5);
                heap.Insert(12);
                heap.Insert(55);
                heap.Insert(3);
                heap.Insert(1);
                heap.Insert(7);
                Console.WriteLine(string.Join(',', heap.Values.Select(item => item.ToString())));
                int min = heap.Min();
                Console.WriteLine(string.Join(',', heap.Values.Select(item => item.ToString())));
                Console.WriteLine("Current Min:" + min);


                min = heap.Min();
                Console.WriteLine("Current Min:" + min);
                Console.WriteLine(string.Join(',', heap.Values.Select(item => item.ToString())));

                min = heap.Min();
                Console.WriteLine("Current Min:" + min);
                Console.WriteLine(string.Join(',', heap.Values.Select(item => item.ToString())));
                min = heap.Min();
                Console.WriteLine("Current Min:" + min);
                Console.WriteLine(string.Join(',', heap.Values.Select(item => item.ToString())));
                min = heap.Min();
                Console.WriteLine("Current Min:" + min);
            }
        }
    }
}
