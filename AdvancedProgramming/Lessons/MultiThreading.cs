
using System.Diagnostics;

namespace AdvancedProgramming.Lessons;

internal class MultiThreading
{
    class Wallet_V1
    {
        public string Name { get; set; }
        public int Bitcoins { get; set; }

        public Wallet_V1(string name, int bitcoins)
        {
            this.Name = name;
            this.Bitcoins = bitcoins;
        }

        public void Debit(int amount)
        {
            Bitcoins -= amount;
        }

        public void Credit(int amount)
        {
            Bitcoins += amount;
        }


        public void RunRandomTransactionSequantailly()
        {
            int[] amouts = { 10, 20, 30, -20, 10, -10, 30, -10, 40, -20 };
            foreach (var amount in amouts)
            {
                var absValue = Math.Abs(amount);
                if (amount < 0)
                    this.Debit(absValue);
                else
                    this.Credit(absValue);

                // ThreadSchedualar : responsible for scheduling threads on processors

                Console.WriteLine($"Thread ID : {Thread.CurrentThread.ManagedThreadId}\tProcessor ID : {Thread.GetCurrentProcessorId()}\t{amount}");

            }
        }

        public override string ToString()
        {
            return $"{Name} -> {Bitcoins} Bitcoins";
        }
    }

    class Wallet_V2
    {
        public Wallet_V2(string name, int bitcoins)
        {
            Name = name;
            Bitcoins = bitcoins;
        }

        public string Name { get; set; }

        public int Bitcoins { get; set; }


        public void Debit(int amount)
        {
            Thread.Sleep(1000);
            Bitcoins -= amount;
            Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId}-{Thread.CurrentThread.Name}\tProcessor ID : {Thread.GetCurrentProcessorId()}\t-{amount}");
        }

        public void Credit(int amount)
        {
            Thread.Sleep(1000);
            Bitcoins += amount;
            Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId}-{Thread.CurrentThread.Name}\tProcessor ID : {Thread.GetCurrentProcessorId()}\t{amount}");
        }

        public void RunRandomTransaction()
        {

            int[] amouts = { 10, 20, 30, -20, 10, -10, 30, -10, 40, -20 };
            foreach (var amount in amouts)
            {
                var absValue = Math.Abs(amount);
                if (amount < 0)
                    this.Debit(absValue);
                else
                    this.Credit(absValue);

                // ThreadSchedualar : responsible for scheduling threads on processors



            }

        }

        public override string ToString()
        {
            return $"{Name} -> {Bitcoins} Bitcoins";
        }
    }

    class Wallet_V3
    {
        public Wallet_V3(string name, int bitcoins)
        {
            Name = name;
            Bitcoins = bitcoins;
        }

        public string Name { get; set; }

        public int Bitcoins { get; set; }

        public void Debit(int amount)
        {
            Thread.Sleep(1000);

            if (Bitcoins >= amount)
            {
                Bitcoins -= amount;
                Thread.Sleep(1000);
            }

        }

        public void Credit(int amount)
        {
            Thread.Sleep(1000);
            Bitcoins += amount;

        }

        public override string ToString()
        {
            return $"Bitcoins : {Bitcoins}";
        }
    }

    class Wallet_V4
    {
        private readonly object lockObj = new object();

        public Wallet_V4(string name, int bitcoins)
        {
            Name = name;
            Bitcoins = bitcoins;
        }

        public string Name { get; set; }

        public int Bitcoins { get; set; }

        public void Debit(int amount)
        {

            lock (lockObj) // any reference type
            {
                if (Bitcoins >= amount)
                {
                    Thread.Sleep(1000);

                    Bitcoins -= amount;
                }
            }


        }

        public void Credit(int amount)
        {
            Thread.Sleep(1000);
            Bitcoins += amount;

        }

        public override string ToString()
        {
            return $"Bitcoins : {Bitcoins}";
        }
    }

    public static void Example1()
    {
        Console.WriteLine($"Process ID : {Process.GetCurrentProcess().Id}");
        Console.WriteLine($"Thread ID : {Thread.CurrentThread.ManagedThreadId} ");
        Console.WriteLine($"Processor ID : {Thread.GetCurrentProcessorId()} ");


    }

    public static void Example2_ThreadCreation()
    {
        Thread t1 = new Thread(() => { while (true) { Console.WriteLine("hola"); Thread.Sleep(1000); } });

        t1.Name = "t1";
        Console.WriteLine(t1.ThreadState);
        t1.Start();
        Console.WriteLine(t1.ThreadState);
        /*
         * another way to run a method 
         Thread t1 = new Thread ( new ThreadStart( methodName ) );
         */
    }

    public static void Example3_BackGroundVsForeGroundThreads()
    {
        int counter = 0;
        Thread.CurrentThread.Name = "Main Thread";
        Thread t1 = new Thread(() => { while (true) { counter++; Console.WriteLine($"{counter}\tForeground Thread Still Running"); ; } });

        t1.Name = "t1";
        t1.IsBackground = true;
        t1.Start();

        Thread.Sleep(5000);
        Console.WriteLine("BackGround Thread Terminated the foreground thread");
    }

    public static void Example4_JoinThread()
    {

        Thread t1 = new Thread(() =>
        {
            for (int i = 1; i <= 10; i++)
            {

                Console.WriteLine($"Thread t1 is running {i}");
                Thread.Sleep(1000);
            }
        });


        Thread t2 = new Thread(() =>
        {
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"Thread t2 is running {i}");
                Thread.Sleep(1000);
            }
        });

        t1.Start();

        // the thread which called this method will be blocked until t1
        // being terminated ( the main thread is the calling thread)

        t1.Join();

        t2.Start();

    }

    public static void Example5_RaceCondition()
    {
        var wallet = new Wallet_V3("Saif", 50);

        var t1 = new Thread(() => { wallet.Debit(40); });
        var t2 = new Thread(() => { wallet.Debit(30); });



        t1.Start();
        t2.Start();

        Console.WriteLine(wallet);


    }


}
