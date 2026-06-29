using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID.SingleResponsibilityPrinciple.After
{
    internal class EmailClient
    {
        // sending email
        public void Send(Account account , string transactionMessage )
        {
            Console.WriteLine($"To : {account.Email} , Name : {account.Name} , Transaction {transactionMessage} ");
        }
    }

}