
using System.Diagnostics;
using System.Reflection;
using System.Data;
using AdvancedProgramming.Lessons;
using System.Text.Json;
using System.Net.Cache;
using System.Net.Http.Headers;

class Program
{

    
    class RangeAttribute : Attribute
    {

        public int Max { get; set; }
        public int Min { get; set; }



        public RangeAttribute(int min, int max)
        {
            this.Max = max;
            this.Min = min;
        }

        public bool IsValid(object? obj)
        {
            if (obj is null) return false;
            
            var value = (int)obj;
            
            return value <= Max && value >= Min;
            
            
        }
        

    }

    class Employee
    {

        [Range(400 , 5000)]
        public int Salary { get; init; }

        [Range(22 , 65)]
        public int Age { get; init; }

        public string Name { get; set; }




    }

    static public void Validate(object ? obj)
    {
        var properties = obj.GetType().GetProperties();

        foreach (var property in properties)
        {

            var attr = property.GetCustomAttribute<RangeAttribute>();

            if (attr != null)
            {
                if (!attr.IsValid(property.GetValue(obj)))
                {
                    Console.WriteLine($"Class Type : {obj.GetType().Name}\tProperty : {property.Name}\tValue : {property.GetValue(obj)}");
                }


            }

        }
    }

    class PersonModel
    {
        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        [Range(18 , 55)]
        public int Age { get; init; }

        [Pattern("aaa@gmail.com")]
        public string Email { get; init;  }
    }
    public static void Main()
    {
        

        var person = new PersonModel { FirstName = null, LastName = "saif", Age = 15, Email = "aaa@gmail.com" };

        List<Error> errors = new List<Error>();
        if (!Validator.Validate(person, errors))
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }








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

