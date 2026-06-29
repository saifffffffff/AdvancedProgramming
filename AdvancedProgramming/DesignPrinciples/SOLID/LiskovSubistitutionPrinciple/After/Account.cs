using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.LiskovSubistitutionPrinciple.After;

abstract class Account
{


    public string Name { get; set; }
    public decimal Balance { get; set; }
    protected Account(string name, decimal balance)
    {
        this.Name = name;
        this.Balance = balance;
    }


    public abstract void Deposit(decimal amout);



}
