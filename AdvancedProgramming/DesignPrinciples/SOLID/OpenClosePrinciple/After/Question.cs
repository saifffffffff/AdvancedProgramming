using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.OpenClosePrinciple.After;

abstract class Question
{

    public string Title { get; set; }

    public int Mark { get; set; }

    public abstract void Print();
}
