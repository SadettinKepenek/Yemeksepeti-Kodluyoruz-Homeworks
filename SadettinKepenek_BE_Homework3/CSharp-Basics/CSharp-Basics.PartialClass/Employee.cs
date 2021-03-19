namespace CSharp_Basics.PartialClass
{
    public partial class Employee
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Employee(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}