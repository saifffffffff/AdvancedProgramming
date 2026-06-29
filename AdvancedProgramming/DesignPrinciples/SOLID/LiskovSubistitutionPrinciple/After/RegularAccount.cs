namespace AdvancedProgramming.DesignPrinciples.SOLID.LiskovSubistitutionPrinciple.After;

abstract class RegularAccount : Account
{
    protected RegularAccount(string name, decimal balance) : base(name, balance)
    {
    }

    public abstract void Withdraw(decimal amout);
}
