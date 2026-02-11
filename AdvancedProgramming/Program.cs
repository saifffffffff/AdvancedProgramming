
using System.Diagnostics;
using System.Reflection;
using System.Data;
using AdvancedProgramming.Lessons;

class Program
{



    internal record Person(string FullName, int Age, string Address);
    internal record Employee(string FullName, int Age, string Address, decimal Salary) : Person(FullName, Age, Address)
    {
        public Employee() : this(default, default, default, default) { }
        public event Action? OnSalaryPaid;
        public enum Role { Junior, Mid, Senior, TeamLeader, ProjectManager, SolutionArchetict }

        public Employee IncreaseSalary(decimal increment)
        {
            if (increment + Salary > 10000) throw new Exception("Salary Exceed busniss rules");

            return new Employee(FullName, Age, Address, Salary + increment);
        }
    };


    class AddressDto
    {
        public string Street { get; set; }
        public string BuildingNo { get; set; }
    }
    
    class EmployeeDto
    {
        public AddressDto Address { get; set; }

        public int key;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }

        public enum Role { Junior, Mid, Sernior }

        public Role TechRole { get; set; }

    }

    class AddressModel
    {
        public string Street { get; set; }
        public string BuildingNo { get; set; }
    }
    
    class EmployeeModel
    {
        public AddressModel Address { get; set; }

        public int key;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }

        public enum Role { Junior, Mid, Sernior }

        public Role TechRole { get; set; }

        private void CalculateSalary()
        {
            Console.WriteLine("Calculated");
        }
    }

    class CheckAttribute : Attribute
    {

    }
    class Processor
    {
        [ObsoleteAttribute("Use ProcessV2 instead", true)]
        public static void Process()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Processing...");
        }

        [CheckAttribute]
        public static void ProcessV2()
        {
            Console.WriteLine("Processing...");
        }
    }

    public static void Main()
    {
       //ObsoleteAttribute attribute = new ObsoleteAttribute();
        // Processor.Process();



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

