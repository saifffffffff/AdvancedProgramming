using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.SingleResponsibilityPrinciple.Before;

class Account
{
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }

    public Account ( string name , string email, decimal balance)
    {
        this.Name = name;
        this.Email = email;
        this.Balance = balance;

    }

    public void MakeTransaction(decimal amount)
    {
        var transactionMessage = string.Empty;

        // handle withdraw

        if (amount < 0)
        {
            if (Balance >= Math.Abs(amount))
            {
                Balance += amount;
                transactionMessage = "Withdraw";
            }
        }
        // handle Deposit
        else
        {
            Balance += amount;
            transactionMessage = "Deposit";
        }

        // sending email

        Console.WriteLine($"To : {Email} , Name : {Name} , Transaction {transactionMessage} ");
    }

}
