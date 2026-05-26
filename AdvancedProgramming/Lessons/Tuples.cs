using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Lessons;

static class Tuples
{

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static Person getPersonInfo() => new Person { Id = 1, Name = "John Doe" }; // returning multiple values using a custom class

    public static string getPersonInfo(out int id)
    {
        id = 1;
        return "John Doe"; // returning multiple values using an out parameter
    } 

    public static Tuple<string , int > getPersonInfoUsingTuple()
    {
        return Tuple.Create("John Doe", 1); // returning multiple values using a tuple
    }

    public static ValueTuple<string , int > getPersonInfoUsingValueTuple()
    {
        return new ValueTuple<string , int> ("John Doe", 1); // returning multiple values using a value tuple
    }

    // C# 7.0 and later supports tuple literals and deconstruction

    // implicit names
    public static ( string  , int ) getPersonInfoUsingTupleLiteral()
    {
        return ("John Doe", 1); // returning multiple values using a tuple literal
    }

    // explicit names
    public static (string Name, int Id) getPersonInfoUsingTupleLiteralWithNames()
    {
        return (Name: "John Doe", Id: 1); 
    }

    // deconstructing tuples into individual variables ( only for value tuples )
    
    public static void TupleDeconstruction() 
    {
        var personInfo = getPersonInfoUsingTupleLiteralWithNames();
        var (name, id) = personInfo;

        var (name2, id2) = getPersonInfoUsingTupleLiteral(); 
        Console.WriteLine($"Name: {name}, Id: {id}");
        Console.WriteLine($"Name: {name2}, Id: {id2}");
    }




}
