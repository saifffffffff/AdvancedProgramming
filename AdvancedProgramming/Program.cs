
using AdvancedProgramming.Algorithms;
using AdvancedProgramming.DataStructures;
using AdvancedProgramming.SOLID.LiskovSubistitutionPrinciple.Before;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;



class Program
{

    public static class Configuration
    {
        public static void ReadFromConfigFile()
        {
            
            string? theme = ConfigurationManager.AppSettings["theme"];
            string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        }

        public static void WriteToConfigFile_AppSettings(string key, string value)
        {
            // edits the configuration file in the 

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var settings = config.AppSettings.Settings;

            if (settings[key] == null)
                settings.Add(key, value);
            else
                settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

        }


    }

    public static void Print<T>(T[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    class Person
    {
        public string name { set; get; }
    }
    public static async Task Main()
    {

        var account = new FixedDepositAccount("saif" , 1000m);



        account.Withdraw(100);
    }



}









// stream is an abstract class

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


