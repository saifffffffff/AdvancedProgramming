namespace AdvancedProgramming.Leasons;

/*
 
Use IDisposable when your class owns a resource that must be released explicitly, especially:

    Unmanaged resources

    Files

    Sockets

    Database connections

    OS handles

Managed objects that implement IDisposable

    FileStream

    HttpClient

    SqlConnection

    Socket

 */

class CurrencyService : IDisposable
{

    bool _disposed;
    HttpClient _httpClient;

    public CurrencyService()
    {
        _httpClient = new HttpClient();
    }

    // true : dispose managed code + unmanaged code
    // false : dispose unmanaged code only
    protected virtual void Dispose( bool disposing )
    {
        if (_disposed)
            return;
        
        if ( disposing)
        {
            _httpClient?.Dispose();
        }

        // unmanaged code
        // in this example there is not unmanaged code

        _disposed = true;


    }

    // Calling Dispose() means “I am DONE with this object.”
    // thats why we release all managed and unmanaged objects
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize( this ); // you dont have to call the finilizer
    }
    
    // if the user forgot to call the dispose method
    ~CurrencyService() { 
        Dispose(false);
    }

    public string GetCurrencies ()
    {
        return _httpClient.GetStringAsync("https://coinbase.com/api/v2/currencies").Result;
    }

}

/*
 example: 

using AdvancedProgramming;

// 1) not recommended


//CurrencyService service = new CurrencyService();
//string result = service.GetCurrencies();
//Console.WriteLine(result);
//service.Dispose();

// 2) recommended

//CurrencyService service = null;

//try
//{
//    service = new CurrencyService();
//    string result = service.GetCurrencies();
//    Console.WriteLine(result);
//}
//catch
//{
//    Console.WriteLine("error");
//}
//finally
//{
//    service?.Dispose();
//}

// 3) more recommended

// using statement used with instaniating objects which implement IDisposable interface
using (CurrencyService service = new CurrencyService())
{
    string result = service.GetCurrencies();
    Console.WriteLine(result);
    // dispose will be called automatically
}
 
 */