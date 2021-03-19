namespace CSharp_Basics.Indexer
{
    public class Person
    {
        private string[] persons = {"John Doe","Jon Snow","Tryion Lannister"};
        
        //indexer
        public string this[int index]
        {
            get => persons[index];
            set => persons[index] = value;
        }
    }
}