using AdvancedProgramming.Leasons;

Person person1 = new Person(null, null);

Console.WriteLine(person1?.FirstName);


class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; } 

    public Person(string fname , string lname)
    {

        FirstName = fname; 
        
        LastName = lname ?? "Y";

    }
}