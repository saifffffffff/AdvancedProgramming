
namespace AdvancedProgramming.Lessons;

public static class OverridingTheObjectClassMethods
{
    
    public class Employee :IEquatable<Employee>
    {

        /// <summary>
        /// i want to make the class immutable
        /// using readonly property will allow 
        /// the user to init the variables from
        /// the constructor only but init
        /// key word will make the use able
        /// to initialize the properties
        /// using the object initializer
        /// </summary>
        public int Id { get; init; } 

        public string? Name { get; init; }

        public decimal Salary { get; init; }
        
        public Employee(int id, string? name, decimal salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }
        
        public Employee() {} // parameterless constructor to be able to use object initializer
        
        public override bool Equals(object? obj)
        {
            Employee? other = obj as Employee;
            return Equals(other);
        }

        public bool Equals(Employee? other)
        {
            if (other is null) return false;
            return other.Id == this.Id;
        }

        public static bool operator ==(Employee? left, Employee? right) { 
        
            if (left is null)
            {
                if ( right is null) 
                    return true;
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator != (Employee? left, Employee? right) => !(left == right);

        // hash function
        public override int GetHashCode()
        {
            return Id.GetHashCode(); // this is the only field used in the equality comparison
        }

        public override string ToString()
        {
            return $"ID : {this.Id} Name : {this.Name} Salary : {this.Salary}";
        }


    }
}
