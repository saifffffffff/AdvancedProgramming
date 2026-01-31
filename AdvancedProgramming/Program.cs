
using System.Numerics;
using System.Text;
using AdvancedProgramming.Lessons;

class program
{
    class Person
    {

    }
    struct car
    {
        public int wheels;
        public int body;
        public Person driver;
    }

    public static void Main ()
    {
        car c = new car ();
        c.wheels = 2;
        c.body = 2;
        c.driver = new Person();
        //c.wheels = 2;

        car c2 = new car();
        c2.wheels = 2;
        c2.body= 2;
        c.driver = new Person();
        
        //c.wheels = 2;


        if (c.Equals(c2))
            Console.WriteLine("Equals");
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

