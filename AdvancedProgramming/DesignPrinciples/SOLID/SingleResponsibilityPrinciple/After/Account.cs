using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.SingleResponsibilityPrinciple.After;

class Account
{
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }

    public Account(string name, string email, decimal balance)
    {
        this.Name = name;
        this.Email = email;
        this.Balance = balance;

    }
}
