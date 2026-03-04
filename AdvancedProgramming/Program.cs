
class Program
{    
    class Money 
    {

        public decimal Amount { get; private set; }

        public Money (decimal amount)
        {
            this.Amount = amount;
        }


        public static Money operator+(Money left , Money right)
        {
            return new Money (left.Amount + right.Amount);
        }

        public static Money operator-(Money left , Money right)
        {
            return new Money (left.Amount - right.Amount);
        }

        public static bool operator ==(Money left , Money right)
        {
            if (left is null)
            {
                if (right is null) return true;

                return false;
            }

            return left.Equals(right);

        }

        public static bool operator != (Money left , Money right) => !(left == right);

        public override bool Equals(object? obj)
        {
            if (obj == null ) throw new NullReferenceException();

            Money m  = (Money)obj;

            return this.Amount == m.Amount;

        }


        public static bool operator <(Money left, Money right)
        {

            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            return left.Amount < right.Amount;

            
        }

        public static bool operator >(Money left, Money right) {
            
            
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);

            return left.Amount > right.Amount;
        }

        public static bool operator <=(Money left, Money right)
        {
            
            if ( left.Equals(right)) return true;

            return left.Amount < right.Amount;
        
        }

        public static bool operator >=(Money left, Money right)
        {

            if (left.Equals(right)) return true;

            return left.Amount > right.Amount;

        }

        public static Money operator++(Money m)
        {
            ArgumentNullException.ThrowIfNull(m);

            return new Money(m.Amount++);

        }
        public static Money operator --(Money m)
        {
            ArgumentNullException.ThrowIfNull(m);

            return new Money(--m.Amount);

        }
    }
    public static async Task Main()
    {
        var money = new Money(50);

        

        Console.WriteLine(money.Amount);
    }

}
















//string emoji = "✊";
//var a = Encoding.UTF32.GetBytes(emoji);
//PrintArray(a);

//string arabic = "س";
//var b  = Encoding.UTF32.GetBytes(arabic);
//PrintArray(b);

//string chinese = "電";
//Console.WriteLine(chinese.Length);
//var c = Encoding.UTF32.GetBytes(chinese);
//PrintArray(c);

//string family = "👨‍👩‍👧‍👦";
//Console.WriteLine();
//Console.WriteLine(family.Length);
//Console.WriteLine(family.Substring( 0 , 1));
////Console.WriteLine(emoji.Length);
//void PrintArray(byte[] arr) { foreach ( var e in arr) Console.Write (e + " "); Console.WriteLine(); }

