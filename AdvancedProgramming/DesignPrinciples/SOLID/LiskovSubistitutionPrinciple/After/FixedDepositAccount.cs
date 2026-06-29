namespace AdvancedProgramming.DesignPrinciples.SOLID.LiskovSubistitutionPrinciple.After;

class FixedDepositAccount : Account
{
    public FixedDepositAccount(string name, decimal amount) : base(name, amount) { }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
    }
}
