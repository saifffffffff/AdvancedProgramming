using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Lessons
{
    internal static class AssemblyProgrammatically
    {

        public static void Trace()
        {
            Console.WriteLine($"Executing Assembly (The assembly where this code lives): {Assembly.GetExecutingAssembly()}");
            Console.WriteLine($"Calling Assembly ( the assembly that called current method ): {Assembly.GetCallingAssembly()}");
            Console.WriteLine($"Entry Assembly (The assembly that started the program execution) : {Assembly.GetEntryAssembly()}");

        }

        public static void CurrentAssemblyFullName()
        {
            var assembly = typeof(Assembly).Assembly;
            Console.WriteLine(assembly.FullName);
        }
        public static void CurrentAssemblyManifestDetailes()
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            Console.WriteLine($"Name: {assemblyName.Name}");
            Console.WriteLine($"Version: {assemblyName.Version}");
            Console.WriteLine($"Culture: {assemblyName.CultureInfo}");
            Console.WriteLine($"Public Key Token: {BitConverter.ToString(assemblyName.GetPublicKeyToken() ?? new byte[0]).Replace("-", "")}");
            //Console.WriteLine($"Code: {assemblyName.CodeBase}");

        }

        public static void CurrentAssemblyLocation()
        {
            Console.WriteLine($"{Assembly.GetExecutingAssembly().Location}");
        }


        public static Stream GetEmbeddedResourceStream(string embeddedResourceName = "names.json")
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream? stream = assembly.GetManifestResourceStream($"AdvancedProgramming.Assets.{embeddedResourceName}"))
            {
                return stream ?? throw new Exception("Resource not found");
            }

            
        }


        public static void WriteAssemblyInfoToConsole(Assembly assembly )
        {
            AssemblyName assemblyName = assembly.GetName();
            Console.WriteLine($"Name : {assemblyName.Name}");
            Console.WriteLine($"Version : {assemblyName.Version}");
            Console.WriteLine($"Path : {assemblyName.CodeBase}");
            Console.WriteLine($"Hash Algorithm : {assemblyName.HashAlgorithm}");
            
        }

        public static void WriteEmbeddedResourceToConsole(string embeddedResourceName = "names.json")
        {
            using Stream stream = GetEmbeddedResourceStream(embeddedResourceName); 
            {
                StreamReader reader = new StreamReader(stream);
                int ByteRead = 0;
                while ((ByteRead = reader.Read()) != -1)
                {
                    Console.Write((char)ByteRead);
                    Thread.Sleep(100);
                }
            }

        }

        public static void WriteEmbeddedResourceNamesToConsole()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string[] resourceNames = assembly.GetManifestResourceNames();

            foreach (string resourceName in resourceNames)
            {
                Console.WriteLine(resourceName);
            }

        }

        public static void WriteEmbeddedResourceInfoToConsole()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var manifestInfo = assembly.GetManifestResourceInfo("AdvancedProgramming.Assets.names.json");

            Console.WriteLine($"Name : {manifestInfo?.FileName} ");
            Console.WriteLine($"Location : {manifestInfo!.ResourceLocation.ToString()}");





        }
    }
}
