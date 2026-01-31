
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Text;
using AdvancedProgramming.Lessons;

class program
{
    
    class Person
    {
        public int age;
        public int tall;
        public int weight;
        public string Name;
        
        public Person(string Name , int age, int tall , int weight)
        {
            this.Name = Name;
            this.age = age;
            this.tall = tall;
            this.weight = weight;
        }
        public override bool Equals(object? obj)
        {
            Person otherPerson = obj as Person;
            if (otherPerson == null) return false;

            return otherPerson.Name == this.Name && otherPerson.age == this.age && this.tall == otherPerson.tall && this.weight == otherPerson.weight;
        }
        public override string ToString()
        {
            return $"Name : {Name} Age : {age} Tall : {tall} Weight : {weight}";
        }
    }

    class Person2 : IEquatable<Person2>
    {
        public int age;
        public int tall;
        public int weight;
        public string Name;

        public Person2(string Name, int age, int tall, int weight)
        {
            this.Name = Name;
            this.age = age;
            this.tall = tall;
            this.weight = weight;
        }
        public override bool Equals(object? obj)
        {
            if  (obj == null) return false;
            return Equals((Person2)obj);
        }

        public bool Equals(Person2 other)
        {
            return other.Name == this.Name && other.age == this.age && this.tall == other.tall && this.weight == other.weight;
        }

        public override string ToString()
        {
            return $"Name : {Name} Age : {age} Tall : {tall} Weight : {weight}";
        }
    }

    public static void IEquatableNotImplemented(long repititions = 10_000_000)
    {
        List<Person> people = new List<Person>();

        Person personToCompare = new Person("saif", 17, 177, 34);

        string[] names = { "saif", "tala", "sara" };
        Random rand = new Random();
        for (int i = 0; i < repititions; i++)
        {
            string randomName = names[rand.Next(0, names.Length)];
            int randomAge = rand.Next(0, 19);
            int randomWeight = rand.Next(20, 40);
            int randomTall = rand.Next(150, 181);
            people.Add(new Person(randomName, randomAge, randomTall, randomWeight));
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int counter = 0;
        foreach (var p in people)
        {
            if (p.Equals(personToCompare))
                counter++;
        }
        stopwatch.Stop();

        Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000m + " seconds " + counter + " match found");
        


    }
    
    public static void IEquatableImplemented(long repitions = 10_000_000)
    {
        List<Person2> people = new List<Person2>();

        Person2 personToCompare = new Person2("saif", 17, 177, 34);

        string[] names = { "saif", "tala", "sara" };
        Random rand = new Random();
        for (int i = 0; i < repitions; i++)
        {
            string randomName = names[rand.Next(0, names.Length)];
            int randomAge = rand.Next(0, 19);
            int randomWeight = rand.Next(20, 40);
            int randomTall = rand.Next(150, 181);
            people.Add(new Person2(randomName, randomAge, randomTall, randomWeight));
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int counter = 0;
        foreach (var p in people)
        {
            if (p.Equals(personToCompare))
                counter++;
        }
        stopwatch.Stop();

        Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000m + " seconds " + counter + " match found");


    }

    public static void Main ()
    {


        IEquatableImplemented(100_000_000);

        IEquatableNotImplemented(100_000_000);


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

