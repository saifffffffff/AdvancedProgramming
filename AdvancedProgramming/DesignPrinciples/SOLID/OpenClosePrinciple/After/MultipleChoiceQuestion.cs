using AdvancedProgramming.SOLID.OpenClosePrinciple.Before;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.OpenClosePrinciple.After;

class MultipleChoiceQuestion : Question
{
    
    public List<string> Choices { get; set; } = new List<string>();


    public override void Print()
    {
        Console.WriteLine(Title);
        Console.WriteLine("--------------------------------");
        foreach ( var choice in Choices)
            Console.WriteLine(choice);
        Console.WriteLine("--------------------------------");
    }

    
}
