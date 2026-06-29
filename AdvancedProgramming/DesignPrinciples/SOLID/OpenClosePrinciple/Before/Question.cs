using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.OpenClosePrinciple.Before;

class Question
{
    public string Title { get; set; }

    public int Mark { get; set; }

    public QuestionType QuestionType { get; set; }

    public List<string> Choices { get; set; } = new List<string>();
}
