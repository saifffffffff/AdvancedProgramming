namespace AdvancedProgramming.DesignPrinciples.SOLID.OpenClosePrinciple.After;

class WHQuestion : Question
{

    public override void Print()
    {

        Console.WriteLine($"Title : {Title} - Mark : {Mark}");
        Console.WriteLine("  _____________________________");
        Console.WriteLine("  _____________________________");
        Console.WriteLine("  _____________________________");
    }
}
