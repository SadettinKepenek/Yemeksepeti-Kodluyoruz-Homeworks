namespace CSharp_Basics.PartialClass
{
    public partial class Employee
    {
        public string GetFullName()
        {
            return $"{Firstname} {Lastname}";
        }
    }
}