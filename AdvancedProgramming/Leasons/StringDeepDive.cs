using System.Text;

namespace AdvancedProgramming.Leasons;

public static class StringDeepDive
{
    // Encoding is the process of mapping numeric code to binary representation
    public static async Task GetDataInAssciFormat()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(path, "asciiNewContent.txt");

        using (var client = new HttpClient())
        {
            var uri = new Uri("https://aljazeera.net/search/feed");
            using (HttpResponseMessage response = await client.GetAsync(uri))
            {
                var byteArray = await response.Content.ReadAsByteArrayAsync();
                string result = Encoding.ASCII.GetString(byteArray);
                File.WriteAllText(filePath, result);
                
            }

        }



    }

    public static async Task GetDataInUTF8ormat()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(path, "utf8NewContent.txt");

        using (var client = new HttpClient())
        {
            var uri = new Uri("https://aljazeera.net/search/feed");
            using (HttpResponseMessage response = await client.GetAsync(uri))
            {
                var byteArray = await response.Content.ReadAsByteArrayAsync();
                string result = Encoding.UTF8.GetString(byteArray);
                File.WriteAllText(filePath, result);

            }

        }



    }


}
