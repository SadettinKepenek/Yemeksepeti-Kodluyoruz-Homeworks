using System;

namespace CSharp_Basics.Value_Reference_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            int toplam = 0;
            Add(5,toplam);
            Console.WriteLine(toplam);
            AddWithRef(5,ref toplam);
            Console.WriteLine(toplam);
        }

        public static void Add(int a,int toplam)
        {
            toplam += a;
        }

        public static void AddWithRef(int a, ref int toplam)
        {
            toplam += a;
        }
    }
}