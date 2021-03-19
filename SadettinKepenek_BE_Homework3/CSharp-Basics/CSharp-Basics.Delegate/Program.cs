using System;

namespace CSharp_Basics.Delegate
{
    class Program
    {
        delegate void NumberDelegate(int a,int b);
        
        public static void Main(string[] args)
        {

            NumberDelegate delegateMethod1 = Add;
            delegateMethod1.Invoke(3,2);
            delegateMethod1(5, 4);
            

            NumberDelegate delegateMethod2 = (a, b) => Multiply(a,b);
            delegateMethod2.Invoke(3,4);

        }

        public static void Add(int a,int b)
        {
            Console.WriteLine($"{a} + {b} = {a+b}");
        }
        public static void Multiply(int a,int b)
        {
            Console.WriteLine($"{a} * {b} = {a*b}");

        }
    }
}