using AdvancedProgramming.SOLID.SingleResponsibilityPrinciple.After;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AdvancedProgramming.DesignPrinciples.SOLID.SingleResponsibilityPrinciple.After;

class AccountService
{

    public void Deposit( Account account , decimal amount  )
    {
        var transactionMessage = "Deposit";

        // handle Deposit
        if ( amount > 0 )
        {
            account.Balance += amount;
            transactionMessage = "Deposit";
        }

        var emailClient = new EmailClient();
        emailClient.Send(account, transactionMessage);

    }

    public void Withdraw(Account account, decimal amount) 
    {

        var transactionMessage = "Withdraw";

        // handle withdraw

        if (amount < 0)
        {
            if (account.Balance >= Math.Abs(amount))
            {
                account.Balance += amount;
                transactionMessage = "Withdraw";
            }
        }

        var emailClient = new EmailClient();
        emailClient.Send(account, transactionMessage);

    }
    
}
