using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Lessons
{
    public static class Equality
    {

        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Person(int Id , string Name) 
            {
                this.Id = Id;
                this.Name = Name;
            }
        }

        struct Car
        {
            public int NumberOfWheels;
            public int DriverId;
        }

        struct License
        {
            public int LicenseId;
            public Person Driver;
        }

        public static void Example ()
        {
            Car car1 = new Car();
            car1.NumberOfWheels = 4;
            car1.DriverId = 1;

            Car car2 = new Car();
            car2.NumberOfWheels = 4;
            car2.DriverId = 1;
            
            Car car3 = new Car();
            car3.NumberOfWheels = 2;
            car3.DriverId = 1;
            // if ( car1 == car2) not allowed until overloaded

            if ( car1.Equals(car2 ) )
                Console.WriteLine("car1 equals car2");
            else
                Console.WriteLine("car 1 not equals car 2");
            
            if ( car1.Equals(car3 ) )
                Console.WriteLine("car 1 equals car 3");
            else
                Console.WriteLine("car 1 not equals car 3");

            Person p1 = new Person(1, "saif");
            Person p2 = new Person(2, "salem");

            if ( p1 == p2 ) Console.WriteLine("p1 equals p2");
            else Console.WriteLine("p1 not equals p2");

            if (p1.Equals(p2)) Console.WriteLine("p1 equals p2");
            else Console.WriteLine("p1 not equals p2");

            License license1 = new License();
            License license2 = new License();

            license1.LicenseId = 1;
            license1.Driver = p1;

            license2.LicenseId = 2;
            license2.Driver = p2;

            if (license2.Equals(license1)) Console.WriteLine("license 1 equals license 2");
            else Console.WriteLine("license 2 not equals license 1");
            



        }

    }
}
