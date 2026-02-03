
using System.Diagnostics;

namespace AdvancedProgramming.Lessons;

public static class PerformanceTests
{
    struct Person
    {
        public int age;
        public int tall;
        public int weight;
        public string Name;

        public Person(string Name, int age, int tall, int weight)
        {
            this.Name = Name;
            this.age = age;
            this.tall = tall;
            this.weight = weight;
        }
       
        public override bool Equals(object? obj)
        {
            if ( obj == null) return false;
            Person otherPerson = (Person)obj ; 

            return otherPerson.Name == this.Name && otherPerson.age == this.age && this.tall == otherPerson.tall && this.weight == otherPerson.weight;
        }

        public override string ToString()
        {
            return $"Name : {Name} Age : {age} Tall : {tall} Weight : {weight}";
        }
    }

    struct Person2 : IEquatable<Person2>
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
            if (obj == null) return false;
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

    public static void IEquatableNotImplemented_SpeedTest(long repititions = 10_000_000)
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

    public static void IEquatableImplemented_SpeedTest(long repitions = 10_000_000)
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

}
