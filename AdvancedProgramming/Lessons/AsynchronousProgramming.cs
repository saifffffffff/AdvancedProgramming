using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Lessons;

public static class AsynchronousProgramming
{
    
    class MyTask
    {
        WaitCallback callback;
        bool isFinished = false;
        protected event Action onCompleted;


        public MyTask(Action action)
        {
            callback = (o) => { action(); onCompleted?.Invoke(); isFinished = true; };
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

        public void Wait()
        {
            while (!isFinished) ;
        }

        public virtual void ContinueWith(Action action)
        {
            onCompleted = action;
        }

        public static void Run(Action action) => new MyTask(action).Start();


    }

    class MyTask<TResult>
    {

        WaitCallback callback;

        event Action<MyTask<TResult>> OnCompleted;
        public bool isCompleted { get; private set; }

        TResult _result;
        public TResult Result { get { this.Wait(); return _result; } }



        public MyTask(Func<TResult> task)
        {
            callback = (o) => { _result = task(); isCompleted = true; OnCompleted?.Invoke(this); };
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

        public static MyTask<TResult> Run(Func<TResult> function)
        {
            MyTask<TResult> task = new(function);
            task.Start();
            return task;
        }

        public void Wait()
        {
            while (!isCompleted) ;
        }

        public void ContinueWith(Action<MyTask<TResult>> action)
        {
            OnCompleted += action;
        }

        public static MyTask<TResult> Delay(int milliSeconds)
        {
            //return new MyTask<TResult> ( () => { Thread.Sleep(millisecondsTimeout); })
            return null;
        }
    }

    public readonly struct MyCancellationToken : IEquatable<MyCancellationToken>
    {
        readonly MyCancellationTokenSource? _source;

        public bool IsCancellationRequested => _source is not null && _source.IsCancellationRequested;

        internal MyCancellationToken(MyCancellationTokenSource? source) => _source = source;


        public void Register(Action callback)
        {
            if (_source is not null)
                _source.Register(callback);
        }


        public void ThrowIfCancellationRequested()
        {

            throw new OperationCanceledException();

        }

        public bool Equals(MyCancellationToken other)
        {
            return other._source == _source;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return Equals((MyCancellationToken)obj);
        }

        public static bool operator ==(MyCancellationToken left, MyCancellationToken right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MyCancellationToken left, MyCancellationToken right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return _source.GetHashCode();
        }



    }

    public class MyCancellationTokenSource
    {


        public bool IsCancellationRequested { get; private set; }
        public MyCancellationToken Token { get; private set; }
        event Action callbacks;

        public MyCancellationTokenSource()
        {
            Token = new MyCancellationToken(this);

        }

        public void Cancel()
        {
            IsCancellationRequested = true;
            callbacks?.Invoke();
        }

        internal void Register(Action callback)
        {
            callbacks += callback;
        }

    }

    public static class TaskExamples
    {

        static int sum(int a, int b) => a + b;

        static int method()
        {
            Thread.Sleep(5000);
            return 10;
        }

        public static void Example1_TaskCreation()
        {
            // task class is an abstraction for the threads
            // task class use the pooled threads
            Task task = new Task(() => { Thread.Sleep(3000); });

            task.Start();

            task.Wait(); // the same as join

            // another way 
            Task.Run(() => { Thread.Sleep(3000); });

        }

        public static void Example2_TaskResult()
        {

            // how to get the returned result from a thread
            int threadResult = default;
            Thread t1 = new Thread(() => { Thread.Sleep(3000); threadResult = sum(3, 4); });
            Console.WriteLine($"Thread result {threadResult}");

            // how to get the returned result from a task
            Task<int> task = new(() => { Thread.Sleep(3000); return sum(3, 4); });
            int taskResult = task.Result; // this will block the main thread until the result is returned from the task in execution
            Console.WriteLine($"Task result {taskResult}");


        }

        public static void LongTask() => Thread.Sleep(3000);

        public static void Example3_LongRunningTask()
        {
            /*
             if the start up time for the task ( allocate stack memory , register thread in thread scheduler , etc. )
             consumes more time than the task execution then use the pooled thread to skip the start 
             up time ( short running task)

             *start up time cost : 0.1ms to 2ms depending on the hardware

             if the execution time consumes more time than the start up time then use the unpooled thread 
             ( long running task )
            */

            Task.Factory.StartNew(() => LongTask(), TaskCreationOptions.LongRunning);
            //TaskFactory factory = new TaskFactory();
            //factory.StartNew(() => LongTask(), TaskCreationOptions.LongRunning);

            Console.WriteLine($"Is Pooled : {Thread.CurrentThread.IsThreadPoolThread}");




        }

        static void ThrowException() { throw new Exception(); }

        static void ThrowExceptionWithTryCatchBlock()
        {
            try
            {
                throw new Exception();
            }
            catch
            {
                Console.WriteLine("Exception thrown");
            }
        }


        public static void Example4_ExceptionPropagation()
        {
            /*
            try
            {
                var t = new Thread(ThrowException);
                t.Start();
                t.Join();
                // it wont catch the exception becaues 
                // the thread responsible for catching the 
                // thread is different than the running thread
            }
            catch
            {
                Console.WriteLine("Exception Thrown");
            }
            */

            // exception will be caught
            var t1 = new Thread(ThrowExceptionWithTryCatchBlock);
            t1.Start();
            t1.Join();

            // if you want to propagate the exception to the thread that runs the thread ( main in our case )
            // use the Task

            try
            {
                Task.Run(ThrowException).Wait();
            }
            catch
            {
                Console.WriteLine("Exception Thrown");
            }
        }

        static bool IsPrimeNumber(int number)
        {
            for (int i = 2; i * i <= number; i++)
                if (number % i == 0)
                    return false;

            return true;
        }
        public static int CountPrimeNumberInRange(int lowerBound, int upperBound)
        {
            int counter = 0;

            for (int number = lowerBound; number < upperBound; number++)
            {
                if (IsPrimeNumber(number))
                    counter++;
            }

            return counter;

        }

        public static void Example5_TaskContinution()
        {

            var awaiter = Task.Run(() => CountPrimeNumberInRange(1, 10_000_000)).GetAwaiter();
            //Console.WriteLine(task.Result); // bad blocks the thread
            awaiter.OnCompleted(() => { Console.WriteLine(awaiter.GetResult()); });

            // or

            var task = Task.Run(() => CountPrimeNumberInRange(1, 10_000_000));
            task.ContinueWith((task) => { Console.WriteLine(task.Result); });

            Console.WriteLine("Hello");


        }

        public static async Task<string> GetWeatherDataAsync()
        {
            using HttpClient client = new HttpClient();
            return await client.GetStringAsync("https://reqres.in/api");
        }

        public static Task<string> ReadContent(string Url)
        {
            var client = new HttpClient();
            var task = client.GetStringAsync(Url); // returns Task<string>
            return task;

        }

        public static async Task<string> DownlaodBigFile(CancellationToken token)
        {
            try
            {
                using HttpClient client = new HttpClient();
                return await client.GetStringAsync("https://httpbin.org/delay/60", token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Downloading cancelled");
                return "Field";
            }
        }


        public static async Task UsingWhenAll()
        {
            var task1 = GetWeatherDataAsync();
            var task2 = ReadContent("https://reqres.in/api");
            var task3 = DownlaodBigFile(new CancellationToken());

            await Task.WhenAll(task1, task2, task3);
            Console.WriteLine($"task1 completed : {task1.Result.Substring(0, Math.Min(200, task1.Result.Length))}");
            Console.WriteLine($"task2 completed : {task1.Result.Substring(0, Math.Min(200, task1.Result.Length))}");
            Console.WriteLine($"task3 completed : {task1.Result.Substring(0, Math.Min(200, task1.Result.Length))}");


        }

        public static async Task UsingWhenAny()
        {
            var task1 = GetWeatherDataAsync();
            var task2 = ReadContent("https://reqres.in/api");
            var task3 = DownlaodBigFile(new CancellationToken());

            var tasks = new List<Task> { task1, task2, task3 };

            while (tasks.Count > 0)
            {

                var finishedTask = await Task.WhenAny(tasks);

                if (finishedTask == task1)
                    Console.WriteLine("Task 1 is finished");

                if (finishedTask == task2)
                    Console.WriteLine("Task 2 is finisehd");

                if (finishedTask == task3)
                    Console.WriteLine("Task 3 is finised");

                tasks.Remove(finishedTask);

            }

        }

        public static async Task ReportProgressAsync(int steps, CancellationToken token = default, IProgress<int>? progress = null)
        {

            try
            {
                for (int i = 1; i <= steps; i++)
                {

                    token.ThrowIfCancellationRequested();
                    await Task.Delay(300);

                    if (i % 10 == 0)
                    {
                        Console.Clear();
                        progress?.Report(i);
                    }

                }
            }

            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation cancelled.");
            }


        }




    }


    public static void ShowThreadInfo(Thread thread, short line)
    {
        Console.WriteLine($"Line#{line}\tThread ID {thread.ManagedThreadId}\tPooled {thread.IsThreadPoolThread}\tBackground {thread.IsBackground}");
    }

    public static void CallSynchronous()
    {
        Thread.Sleep(4000);
        ShowThreadInfo(Thread.CurrentThread, 260);
        Task.Run(() => Console.WriteLine("+++++++++++ Synchronous +++++++++++")).Wait();
    }

    public static void CallAsynchronous()
    {
        ShowThreadInfo(Thread.CurrentThread, 266);

        Task.Delay(4000).GetAwaiter().OnCompleted(() =>
        {
            ShowThreadInfo(Thread.CurrentThread, 269);
            Console.WriteLine("+++++++++++++ Asynchronous +++++++++++");
        });

    }

    public static void AsyncVsSyncExample()
    {
        ShowThreadInfo(Thread.CurrentThread, 278);

        CallSynchronous();

        ShowThreadInfo(Thread.CurrentThread, 282);
        CallAsynchronous();

        ShowThreadInfo(Thread.CurrentThread, 285);
        Console.ReadKey();
    }

    static class CancellationTokenExamples
    {
        public static async Task CountAsync01(MyCancellationToken token)
        {

            try
            {
                for (int i = 0; i <= 20; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine(i);
                    await Task.Delay(300);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation requested");
            }
        }

        public static async Task CountAsync02(CancellationToken token)
        {

            try
            {
                for (int i = 0; i <= 20; i++)
                {

                    Console.WriteLine(i);
                    await Task.Delay(300, token);
                }
            }
            catch
            {
                Console.WriteLine("Cancellation requested");
            }

        }

        public static async Task CountDownAsync(int seconds, CancellationToken token)
        {
            try
            {
                for (int i = seconds; i > 0; i--)
                {
                    Console.WriteLine(i);
                    await Task.Delay(1000, token);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation Requested");
            }

        }

        public static async Task DownloadFile(CancellationToken token)
        {

            try
            {
                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine($"Part {i} Started Downloading");
                    await Task.Delay(1500, token);
                    Console.WriteLine($"Part {i} Finished Downloading");
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation Requested");
            }
        }

        public static async Task StillAlive(CancellationToken token)
        {


            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Still Alive");
                await Task.Delay(800);
            }

            Console.WriteLine("Worker stopped gracefully");

        }

        public static async Task<string> FetchWithTimeout(int timeoutSeconds, CancellationToken token)
        {
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));

            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, timeoutCts.Token); // linkes many tokens with each other

            CancellationToken combinedToken = linkedCts.Token;

            try
            {
                using HttpClient client = new HttpClient();

                return await client.GetStringAsync("https://httpbin.org/delay/4", combinedToken);

            }

            catch (OperationCanceledException)
            {
                if (timeoutCts.IsCancellationRequested)
                {
                    throw new TimeoutException();
                }
                else if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }
                else throw;
            }

            catch (HttpRequestException ex)
            {
                throw new Exception("Network error during fetching");

            }

        }

        public static async Task Dumb(CancellationToken token)
        {
            try
            {
                await Task.Delay(2000, token);

                using HttpClient client = new HttpClient();

                await client.GetStringAsync("https://httpbin.org/delay/5", token);

                await Task.Delay(3000, token);

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Dumb Operation Cancelled");
            }

        }

        public static async Task ProcessItemsAsync(IEnumerable<int> items, CancellationToken token)
        {
            try
            {
                int counter = 1;
                foreach (int process in items)
                {
                    Console.WriteLine($"Processing {counter} ");
                    await Task.Delay(700, token);
                    counter++;
                }
            }
            catch
            {
                Console.WriteLine("Cancelled");
            }

        }



    }
}

// NOTES : 

// async is written because in the old projects 'await' could be a variable name so in order to avoid errors async key word is required to use the await key word in the async context

// await is a syntactic sugar for 
/*

await Test();
DoSomthing();

==

Test().ContinueWith ( t => DoSomthing )

 */

// async programming strength appear in UI Application not console ones because event driven programming's performance can be improved by async programming 

// Task<somthing> is a promise to return the 'somthing' thats why it is used in the aysn context

// if the main thread finisehd before the async method continue the task it wont be waiting for the task unti it finish instead the process will be terminated

// async programming used in IO bound operations - Api calls - Database connectons - File IO operations ( external resources ) ( unmanaged code ) 

// when you see a method with the suffix ( Async ) then internally the await keyword is used to call an async method and whenever the await is  
// used the control will be return to the caller and once the awaited task is completed the control return to that method executes the remained 
// code in the method and then returned to the caller 

//Execution is based on external resource allocation and when tasks complete

// Key missing feature: starting multiple independent tasks at once and awaiting them together (using Task.WhenAll, WhenAny, or awaiting in parallel).

