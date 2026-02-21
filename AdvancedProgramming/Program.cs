
using System.Diagnostics;
using System.Reflection;
using System.Data;
using AdvancedProgramming.Lessons;
using System.Text.Json;
using System.Net.Cache;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

class Program
{

    
    


   

    public static void method()
    {
        Thread.Sleep(5000);

    }

    public static void Main()
    {


        var timer = new Stopwatch();
        timer.Start();
        
        for (int i = 0; i < 10; i++)
        {
            new Thread(method).Start();
        }
        
        Thread.Sleep(10000);
        timer.Stop();
        Console.WriteLine( ( timer.Elapsed.TotalNanoseconds - TimeSpan.FromSeconds(10).TotalNanoseconds ) / 1_000_000_000);
        
        Console.WriteLine("- - - - - - - - - - - - - - - - -  - - - -");

        timer.Restart();
        timer.Start();
        for (int i = 0; i < 10; i++)
            ThreadPool.QueueUserWorkItem((obj) => { method(); });
        
        Thread.Sleep(10000);
        timer.Stop();
        Console.WriteLine((timer.Elapsed.TotalNanoseconds - TimeSpan.FromSeconds(10).TotalNanoseconds ) / 1_000_000_000);

    }

}
    











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

