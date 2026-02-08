
using System.Diagnostics.CodeAnalysis;
using System.Text;
using AdvancedProgramming.Lessons;
using static AdvancedProgramming.Lessons.OverridingTheObjectClassMethods;
using DVLD_Application.Entities;
using System.IO;
using System.Reflection;

class Program
{
    
    



    public static void Main()
    {

        Assembly assembly = Assembly.GetExecutingAssembly();
        Stream? stream = assembly.GetManifestResourceStream("AdvancedProgramming.Assets.names.json");

        StreamReader reader = new StreamReader(stream);

        int byteRead = 0;
        while ( (byteRead = reader.Read())  != -1)
        {
            Console.Write((char)byteRead);
            Thread.Sleep(300);
        }


        















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

