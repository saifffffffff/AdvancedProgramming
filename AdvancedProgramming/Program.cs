
using AdvancedProgramming.DataStructures;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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


    //delegate void Swap<T>(ref T a, ref T b);
    
    class Person : IComparable
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public int CompareTo(object? obj)
        {
            if (obj is Person other)
                return string.Compare(this.Phone, other.Phone);

            throw new ArgumentException("Object is not a Person");
        }

    }

    
    class Message
    {
        public string RoomKey { get; set; }
        public byte[] Data { get; set; }
    }

    class Room
    {
        public void BroadCastMessage(Message message)
        {
            // send the message to all clients in the room
        }
    }

    public static async Task Udp()
    {
        var client = new UdpClient();

        Dictionary<string, Room> rooms = new();

        rooms.Add("abc", new Room());
        rooms.Add("123", new Room());
        rooms.Add("saif", new Room());


        Console.WriteLine("Press Enter to send a message, or any other key to receive:");

        var key = Console.ReadKey();

        // send - client

        if (key.Key == ConsoleKey.Enter)
        {
            client.Connect("127.0.0.1", 5000);

            Console.WriteLine("Room Key : ");
            string roomKey = Console.ReadLine();
            Console.WriteLine("Message : ");
            string Message = Console.ReadLine();

            var message = new Message
            {
                RoomKey = roomKey,
                Data = Encoding.UTF32.GetBytes(Message),
            };

            string json = JsonSerializer.Serialize(message);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await client.SendAsync(bytes, bytes.Length);

            await client.ReceiveAsync().ContinueWith(t =>
            {
                var response = t.Result;
                string responseMessage = Encoding.UTF8.GetString(response.Buffer);
                Console.WriteLine("Response from server: " + responseMessage);
            });

        }


        // receive ( server mode ) 
        else
        {
            client = new UdpClient(5000);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 5000);

            byte[] data = client.Receive(ref ip);

            string json = Encoding.UTF8.GetString(data);

            var message = JsonSerializer.Deserialize<Message>(json);
            var room = rooms[message.RoomKey];

            room.BroadCastMessage(message);

            client.Send(Encoding.UTF8.GetBytes("Message received"), ip);




        }

    }

    public static void Swap(int[] arr , int i , int j )
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }


    //public static int Partition(int[] arr , int l , int h )
    //{

    //    int p = arr[h - 1];
    //    int j = l - 1;

    //    for ( int i = l; i < h ; i++)
    //    {

    //        if (arr[i] < p)
    //        {
    //            j++;
    //            Swap(arr, j, i);
    //        }

    //    }

    //    Swap(arr, j + 1, h - 1);

    //    return j + 1;

    //}
    //public static void QuickSort( int[] arr , int l , int h )
    //{

    //    if ( Math.Abs( l - h ) <= 1)
    //        return;

    //    int pivot = Partition(arr, l, h);

    //    QuickSort( arr , 0 , pivot - 1 ); // [0 , p)
    //    QuickSort(arr, pivot + 1 , h ); // (p , end]


    //}


    // Partition (Lomuto scheme)
    static int Partition(int[] A, int p, int r)
    {
        int x = A[r];      // pivot
        int i = p - 1;

        for (int j = p; j < r; j++)
        {
            if (A[j] <= x)
            {
                i++;
                Swap(A, i, j);
            }
        }

        Swap(A, i + 1, r);
        return i + 1;
    }

    // Quick Sort
    static void QuickSort(int[] A, int p, int r)
    {
        if (p < r)
        {
            int q = Partition(A, p, r);

            QuickSort(A, p, q - 1);   // left side
            QuickSort(A, q + 1, r);   // right side
        }
    }


    public class PrologEngine
    {
        public Action<string>? OnOuputRecieved;
        public Func<string>? OnInputRequested;

        Process _prolog;

        public PrologEngine( string swiplPath , string projectPath)
        {

            _prolog = new Process();
            
            try
            {
                _prolog.StartInfo = new () {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = swiplPath,
                    Arguments = projectPath
                };
            }
            catch (InvalidOperationException) 
            {
                throw new Exception("invalid path");
            }


        }

        async Task ReadFromProlog ()
        {
            while (!_prolog.HasExited)
            {
                string? line = await _prolog.StandardOutput.ReadLineAsync();
                
                if ( !string.IsNullOrWhiteSpace(line))
                    OnOuputRecieved?.Invoke(line);
            }
        }


        async Task WriteToProlog(string str)
        {
            await _prolog.StandardInput.WriteLineAsync(str);
            await _prolog.StandardInput.FlushAsync();
        }

        public async Task Run ()
        {
            
            
            _prolog.Start();
            _ = Task.Run(ReadFromProlog); // creating new thread for reading from prolog process

            await WriteToProlog("start.");

            while ( true )
            {
                string? userInput = OnInputRequested?.Invoke();
                
                if (string.IsNullOrWhiteSpace(userInput))
                    continue;

                await WriteToProlog(userInput);

            }

        }
        

    }






    public static async Task Main()
    {


        PrologEngine prolog = new PrologEngine(@"C:\Program Files\swipl\bin\swipl.exe" , @"C:\Users\tareq\OneDrive\Pictures\Screenshots\Desktop\university\AI\PrologProject.pl");
        
        prolog.OnOuputRecieved += (output) =>
        {
            Console.WriteLine(output);
        };

        prolog.OnInputRequested += () =>
        {
            string input = Console.ReadLine();
            return input;
        };

        await prolog.Run();

        //var  prolog = new Process() ;
        //prolog.StartInfo = psi;
        //prolog.Start();

        
        //// Read Prolog output continuously
        //_ = Task.Run(async () =>
        //{
        //    while (!prolog.StandardOutput.EndOfStream)
        //    {
        //        string line = await prolog.StandardOutput.ReadLineAsync();

        //        if (!string.IsNullOrWhiteSpace(line))
        //        {
        //            Console.WriteLine(line);
        //        }
        //    }
        //});

        //// Read Prolog errors continuously
        //_ = Task.Run(async () =>
        //{
        //    while (!prolog.StandardError.EndOfStream)
        //    {
        //        string line = await prolog.StandardError.ReadLineAsync();

        //        if (!string.IsNullOrWhiteSpace(line))
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine(line);
        //            Console.ResetColor();
        //        }
        //    }
        //});

        //// Start your expert system
        //await prolog.StandardInput.WriteLineAsync("start.");
        //await prolog.StandardInput.FlushAsync();

        //// Forward user input to Prolog
        //while (true)
        //{
        //    string userInput = Console.ReadLine();

        //    if (userInput == "exit")
        //    {
        //        await prolog.StandardInput.WriteLineAsync("halt.");
        //        break;
        //    }

        //    await prolog.StandardInput.WriteLineAsync(userInput);
        //    await prolog.StandardInput.FlushAsync();
        //}

        //prolog.WaitForExit();


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


