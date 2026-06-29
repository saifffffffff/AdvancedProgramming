using AdvancedProgramming.SOLID.OpenClosePrinciple.Before;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.OpenClosePrinciple.After;

 class Quiz
{

    public List<Question> Questions { get; }

    public Quiz(List<Question> questions)
    {
        this.Questions = questions;
    }

    public void Print()
    {
        foreach( var question in Questions)
        {
            question.Print();
        }

    }
}
