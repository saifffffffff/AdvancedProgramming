using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Lessons;

public static class OperatorOverloading
{
    class Money
    {

        public decimal Amount { get; private set; }

        public Money(decimal amount)
        {
            this.Amount = amount;
        }


        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Amount + right.Amount);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Amount - right.Amount);
        }

        public static bool operator ==(Money left, Money right)
        {
            if (left is null)
            {
                if (right is null) return true;

                return false;
            }

            return left.Equals(right);

        }

        public static bool operator !=(Money left, Money right) => !(left == right);

        public override bool Equals(object? obj)
        {
            if (obj == null) throw new NullReferenceException();

            Money m = (Money)obj;

            return this.Amount == m.Amount;

        }

        public override int GetHashCode() => (int)this.Amount;


        public static bool operator <(Money left, Money right)
        {

            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            return left.Amount < right.Amount;


        }

        public static bool operator >(Money left, Money right)
        {


            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            return left.Amount > right.Amount;
        }

        public static bool operator <=(Money left, Money right)
        {

            if (left.Equals(right)) return true;

            return left.Amount < right.Amount;

        }

        public static bool operator >=(Money left, Money right)
        {

            if (left.Equals(right)) return true;

            return left.Amount > right.Amount;

        }

        public static Money operator ++(Money m)
        {
            ArgumentNullException.ThrowIfNull(m);

            return new Money(++m.Amount);

        }
        public static Money operator --(Money m)
        {
            ArgumentNullException.ThrowIfNull(m);

            return new Money(--m.Amount);

        }

        public void operator +=(decimal amountToAdd)
        {
            Amount += amountToAdd;
        }

        public void operator -=(decimal amountToAdd)
        {
            Amount -= amountToAdd;
        }


    }

}
