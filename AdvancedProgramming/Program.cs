using System.Text;
using AdvancedProgramming.Leasons;


//await Task.Run(() => StringDeepDive.GetDataInUTF8ormat());

//Console.WriteLine(Environment.CurrentDirectory);



string emoji = "✊";
var a = Encoding.UTF32.GetBytes(emoji);
PrintArray(a);

string arabic = "س";
var b  = Encoding.UTF32.GetBytes(arabic);
PrintArray(b);

string chinese = "電";
Console.WriteLine(chinese.Length);
var c = Encoding.UTF32.GetBytes(chinese);
PrintArray(c);

string family = "👨‍👩‍👧‍👦";
Console.WriteLine();
Console.WriteLine(family.Length);
Console.WriteLine(family.Substring( 0 , 1));
//Console.WriteLine(emoji.Length);
void PrintArray(byte[] arr) { foreach ( var e in arr) Console.Write (e + " "); Console.WriteLine(); }

//string name = "saif";
//byte[] bytes = { (byte)'s', (byte)'a', (byte)'i', (byte)'f' };
//Console.WriteLine("In Ascii");
//Console.WriteLine(Encoding.ASCII.GetString(bytes));
//PrintArray(Encoding.ASCII.GetBytes(name));
//Console.WriteLine("In UTF 8");
//Console.WriteLine(Encoding.UTF8.GetString(bytes));
//PrintArray(Encoding.UTF8.GetBytes(name));
//Console.WriteLine("UniCode");
//Console.WriteLine(Encoding.Unicode.GetString(bytes));
//PrintArray(Encoding.Unicode.GetBytes(name));

class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; } 

    public Person(string fname , string lname)
    {
        
        FirstName = fname ?? "X"; 
        
        LastName = lname ?? "Y";

    }
}