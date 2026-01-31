
namespace AdvancedProgramming.Lessons;

public static class OverrideEquals_OverrideGetHashcode
{
    /// <summary>
    /// if you override the Equals method then you should override the 
    /// Person .GetHashCode() because even of the objects are logically 
    /// Equal the .GetHashCode() will return a reference based integer
    /// which will make dealing with this object is broken
    /// </summary>
    class Person_1 
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public Person_1(int Id , string Name)
        {
            this.Name = Name;
            this.Id = Id;
        }

        public override bool Equals(object? obj)
        {
            Person_1 otherPerson = obj as Person_1; // if type casting is not compitable it returns null

            if ( otherPerson is null) return false;

            return this.Id == otherPerson.Id && otherPerson.Name == this.Name;

        }

    }

    class Person_2
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person_2(string Name, int Id)
        {
            this.Name = Name;
            this.Id = Id;
        }
        public override bool Equals(object? obj)
        {
            Person_2 otherPerson = obj as Person_2; // if type casting is not compitable it returns null

            if (otherPerson is null) return false;

            return this.Id == otherPerson.Id && otherPerson.Name == this.Name;

        }

        // fields that take place in the equals method impelementation
        // should take place in GetHashCode only ( no less or more )
        public override int GetHashCode()
        {
            // this is a hash function based of multiple parameters
            return HashCode.Combine(this.Id, this.Name);
        }


    }

    // using the IEquatable interface

    class Person_3 : IEquatable<Person_3>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person_3(string Name, int Id)
        {
            this.Name = Name;
            this.Id = Id;
        }
        public override bool Equals(object? obj)
        {
            Person_2? otherPerson = obj as Person_2; // if type casting is not compitable it returns null

            return Equals(otherPerson);

        }
        public bool Equals(Person_3? other)
        {
            if (other is null) return false;

            return other.Id == this.Id && other.Name == this.Name;
        }

        // fields that take place in the equals method impelementation
        // should take place in GetHashCode only ( no less or more )
        public override int GetHashCode()
        {
            // this is a hash function based of multiple parameters
            return HashCode.Combine(this.Id, this.Name);
        }

    }




}
