
using System.Numerics;
using System.Text;
using AdvancedProgramming.Lessons;

class program
{
    class Person
    {
        public string? Name;
        public string? Address;
    }
    struct car
    {
        public int wheels;
        public int body;
        public Person driver;
    }

    public static void Main ()
    {
        
        Person person = new Person ();

        person.Name = "saif";
        person.Address = "123 street";

        Console.WriteLine(person.GetHashCode());

        Person person2 = new Person ();
        person2.Name = "khaled";
        person2.Address = "123 shallow street";

        Console.WriteLine(person2.GetHashCode());


    }


}























//for (int i = 0; i <= 8; i ++)
//    Console.WriteLine(name.Substring(0, i));









//string emoji = "✊";
//var a = Encoding.UTF32.GetBytes(emoji);
//PrintArray(a);

//string arabic = "س";
//var b  = Encoding.UTF32.GetBytes(arabic);
//PrintArray(b);

//string chinese = "電";
//Console.WriteLine(chinese.Length);
//var c = Encoding.UTF32.GetBytes(chinese);
//PrintArray(c);

//string family = "👨‍👩‍👧‍👦";
//Console.WriteLine();
//Console.WriteLine(family.Length);
//Console.WriteLine(family.Substring( 0 , 1));
////Console.WriteLine(emoji.Length);
//void PrintArray(byte[] arr) { foreach ( var e in arr) Console.Write (e + " "); Console.WriteLine(); }

