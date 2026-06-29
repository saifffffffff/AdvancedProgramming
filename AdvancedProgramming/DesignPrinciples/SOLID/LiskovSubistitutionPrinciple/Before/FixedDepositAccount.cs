namespace AdvancedProgramming.DesignPrinciples.SOLID.LiskovSubistitutionPrinciple.Before;

class FixedDepositAccount : Account
{
    public FixedDepositAccount(string name, decimal balance) : base(name, balance)
    {
    }

    public override void Deposit(decimal amout)
    {
        Balance += amout;
    }

    public override void Withdraw(decimal amout)
    {
        throw new NotImplementedException("You can not withdraw from fixed deposit Account !!! ");
    }
}
