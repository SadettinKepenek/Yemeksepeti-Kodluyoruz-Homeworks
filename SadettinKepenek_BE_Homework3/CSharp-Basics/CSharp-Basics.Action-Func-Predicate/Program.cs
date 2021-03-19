using System;

namespace CSharp_Basics.Action_Func_Predicate
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string, string> act = PrintFullName;
            act("John","Doe");

            Func<string,string> func = ReplaceSpaces;
            var result = func(" Test  ");
            Console.WriteLine(result);

            Predicate<string> isUpper = IsUpperCase;
            var predicateResult = isUpper("TEST");
            Console.WriteLine($"Is String UpperCase ? : {predicateResult}");
            
        }

        static void PrintFullName(string firstName,string lastName)
        {
            Console.WriteLine($"{firstName} {lastName}");
        }

        static string ReplaceSpaces(string str)
        {
            return str.Replace(" ", "");
        }

        static bool IsUpperCase(string str)
        {
            return str.Equals(str.ToUpper());
        }
    }
}