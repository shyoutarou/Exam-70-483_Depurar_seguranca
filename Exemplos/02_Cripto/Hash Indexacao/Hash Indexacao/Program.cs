using System;
using System.Collections.Generic;

namespace Hash_Indexacao
{

    class Set<T>
    {
        private List<T> list = new List<T>();
        public void Insert(T item)
        {
            if (!Contains(item))
                list.Add(item);
        }
        public List<T> Itens()
        {
            return list;
        }
        public bool Contains(T item)
        {
            foreach (T member in list)
                if (member.Equals(item))
                    return true;
            return false;
        }
    }

    class SetHash<T>
    {
        private List<T>[] buckets = new List<T>[100];
        public void Insert(T item)
        {
            int bucket = GetBucket(item.GetHashCode());
            if (Contains(item, bucket))
                return;
            if (buckets[bucket] == null)
                buckets[bucket] = new List<T>();
            buckets[bucket].Add(item);
        }
        public Dictionary<int, string> Itens()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < 100; i++)
            {
                if (buckets[i] != null)
                {
                    dict.Add(i, buckets[i][0].ToString().ToString());
                }
            }
            return dict;
        }
        public bool Contains(T item)
        {
            return Contains(item, GetBucket(item.GetHashCode()));
        }
        private int GetBucket(int hashcode)
        {
            // Um código Hash pode ser negativo. Para garantir que você  termine com um 
            // valor positivo, converta o valor para um int não assinado. O bloco unchecked 
            // garante que você possa converter um valor maior que int em int com segurança.
            unchecked
            {
                return (int)((uint)hashcode % (uint)buckets.Length);
            }
        }
        private bool Contains(T item, int bucket)
        {
            if (buckets[bucket] != null)
                foreach (T member in buckets[bucket])
                    if (member.Equals(item))
                        return true;
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var setar = new Set<string>();

            setar.Insert("texto 1");
            setar.Insert("texto 2");
            setar.Insert("texto 1");
            var valida = setar.Contains("texto 1");
            Console.WriteLine(valida); //True
            valida = setar.Contains("texto 3");
            Console.WriteLine(valida); //False

            foreach (var item in setar.Itens())
            {
                //texto 1
                //texto 2
                Console.WriteLine(item);
            }


            var setahash = new SetHash<string>();

            setahash.Insert("texto 1");
            setahash.Insert("texto 2");
            setahash.Insert("texto 1");
            var validahash = setahash.Contains("texto 1");
            Console.WriteLine(validahash); //True
            validahash = setahash.Contains("texto 3");
            Console.WriteLine(validahash); //False

            foreach (var item in setahash.Itens())
            {
                //70 - texto 1
                //97 - texto 2
                Console.WriteLine(string.Format("{0} - {1}", item.Key, item.Value));
            }

            Console.ReadKey();
        }
    }
}
