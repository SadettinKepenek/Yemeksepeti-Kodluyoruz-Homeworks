using System;

namespace CSharp_Basics.Dynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            object a = 10;
            Console.WriteLine(a.GetType());
            
            //Aşağıdaki kod hata verir
            // a = a + 5;

            dynamic b = 10;
            Console.WriteLine(b.GetType());
            b = b + 5;
            Console.WriteLine(b);
        }
    }
}