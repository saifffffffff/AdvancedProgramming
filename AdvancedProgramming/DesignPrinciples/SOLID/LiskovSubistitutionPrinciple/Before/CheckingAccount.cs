using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.LiskovSubistitutionPrinciple.Before;

class CheckingAccount : Account
{
    public CheckingAccount(string name , decimal balance ) : base ( name , balance)
    {

    }

    public override void Deposit(decimal amout)
    {
        Balance += amout;
    }

    public override void Withdraw(decimal amout)
    {
        if ( amout > 1000 )
        {
            Console.WriteLine("You cant withdraw from Fixed Deposit Account!!!");
        }

        Balance -= amout;
    }
}
