
namespace AdvancedProgramming.Lessons;

public static class Records
{
    // the same
    public class Employee_Immutable : IEquatable<Employee_Immutable>
    {

        public int Id { get; init; }

        public string? Name { get; init; }

        public decimal Salary { get; init; }

        public Employee_Immutable(int id, string? name, decimal salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }


        public override bool Equals(object? obj)
        {
            Employee_Immutable? other = obj as Employee_Immutable;
            return Equals(other);
        }

        public bool Equals(Employee_Immutable? other)
        {
            if (other is null) return false;
            return other.Id == this.Id;
        }

        public static bool operator ==(Employee_Immutable? left, Employee_Immutable? right)
        {

            if (left is null)
            {
                if (right is null)
                    return true;
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Employee_Immutable? left, Employee_Immutable? right) => !(left == right);

        public override int GetHashCode()
        {
            return Id.GetHashCode(); // this is the only field used in the equality comparison
        }

        public override string ToString()
        {
            return $"ID : {this.Id} Name : {this.Name} Salary : {this.Salary}";
        }


    }

    record Employee_ReferenceTypeRecord(int Id, string Name, decimal Salary);
    //
    
    // the same - non positional record
    class Employee_Mutable: IEquatable<Employee_Mutable>
    {

        public int Id { get; set; }

    public string? Name { get; set; }

    public decimal Salary { get; set; }

    public Employee_Mutable(int id, string? name, decimal salary)
    {
        Id = id;
        Name = name;
        Salary = salary;
    }


    public override bool Equals(object? obj)
    {
        Employee_Mutable? other = obj as Employee_Mutable;
        return Equals(other);
    }

    public bool Equals(Employee_Mutable? other)
    {
        if (other is null) return false;
        return other.Id == this.Id;
    }

    public static bool operator ==(Employee_Mutable? left, Employee_Mutable? right)
    {

        if (left is null)
        {
            if (right is null)
                return true;
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Employee_Mutable? left, Employee_Mutable? right) => !(left == right);

    public override int GetHashCode()
    {
        return Id.GetHashCode(); // this is the only field used in the equality comparison
    }

    public override string ToString()
    {
        return $"ID : {this.Id} Name : {this.Name} Salary : {this.Salary}";
    }


}

    record Employee_RefTypeRecord_NonPositional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public Employee_RefTypeRecord_NonPositional(int Id, string Name, decimal Salary)
        {
            this.Id = Id;
            this.Name = Name;
            this.Salary = Salary;
        }

    }



}
