
namespace AdvancedProgramming.Lessons;
using Microsoft.Win32;
using System.Text.Json;

#pragma warning disable 

public static class Configuration
{

    
    public static class WindowsRegistry
    {
        private static string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\AdvancedProgramming";
        private static string valueName = "ConsoleColor";

        public static void WriteToRegisrty ()
        {
            
            try
            {
                string serializedValue = JsonSerializer.Serialize(ConsoleColor.Cyan);                
                Registry.SetValue(keyPath, valueName, serializedValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ReadFromRegistry ()
        {
            try
            {
                string? value = Registry.GetValue(keyPath, valueName, null) as string;

                Console.ForegroundColor = value is not null ? JsonSerializer.Deserialize<ConsoleColor>(value) : ConsoleColor.Black;                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }


}
