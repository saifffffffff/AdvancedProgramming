
namespace AdvancedProgramming.Leasons;

public static class NullableReferenceTypes
{
    class Person
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        Person(string fname, string lname)
        {
            // here the FirstName - LastName will never be null
            FirstName = fname ?? "X";
            LastName = lname ?? "Y";
        }

        public void Example1()
        {
            Person p = new Person(null, null); // warning
            
            string name = null;
            
            Console.Write(name.Length);

            int len1 = p.FirstName.Length; // warninig because sometimes the compiler can not see the internal code
            int len2 = p.FirstName!.Length; // overrides the warning behavioar and remove the warninig
            int? len3 = p.FirstName?.Length; // the conditional null operators returns null if null exception thrown

            string name1 = "saif";
            IsLongName(name1);

            string name2 = null;
            IsLongName(name2); // the compiler warn you

            string? name3 = null;
            IsLongName(name3);

            IsLongNameNullable(name3); // no warnings

            IsLongNameWithValidation(name3); // also shows a warninig ( because name3 already has a warning no yellow line appear but you can see the error clicking on the variable ) 
                                             // althoug we check for the name if null or not the compiler cant see these types of validations it only deals with comparing types 
        }

        bool IsLongName(string name) {
            return name.Length > 10;
        }

        bool IsLongNameWithValidation(string name) { 
            
            if ( name == null ) return false;
            
            return name.Length > 10;
        }

        bool IsLongNameNullable (string? name)
        {
            if (name is null) return false;

            return name.Length > 10;
        }
    }
}
