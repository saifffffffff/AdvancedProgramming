
using AdvancedProgramming.Leasons;

string name = "saif";

string name2 = "saif";

string name3 = new string("saif");

Console.WriteLine(string.IsInterned(name3));


bool isRefEqual = string.ReferenceEquals(name, name2);
bool isRefEqual2 = string.ReferenceEquals(name3 , name2);

Console.WriteLine(isRefEqual);
Console.WriteLine(isRefEqual2);

















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

