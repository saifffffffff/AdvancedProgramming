namespace AdvancedProgramming.DesignPrinciples.SOLID.LiskovSubistitutionPrinciple.After;

class SavingAccount : RegularAccount
{

    public SavingAccount(string name, decimal balance) : base(name, balance)
    {

    }
    public override void Deposit(decimal amout)
    {
        Balance += amout;
    }

    public override void Withdraw(decimal amout)
    {
        Balance -= amout;
    }



}