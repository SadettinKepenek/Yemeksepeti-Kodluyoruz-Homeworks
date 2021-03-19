using System;

namespace CSharp_Basics.Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            var firtPerson = p[0];
            Console.WriteLine(firtPerson);
        }
    }
}