using System;
using System.Diagnostics;
using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratorOptions options;

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Reading options...");

            stopwatch.Start();
            using (Stream inputStream = File.OpenRead("TextFileGenerator.xml"))
            {
                OptionsSerializer serializer = new OptionsSerializer();
                options = serializer.Deserialize(inputStream);
            }
            stopwatch.Stop();

            Console.WriteLine("Read options: " + stopwatch.Elapsed);

            Console.WriteLine("Generating file...");
            using (Stream outputStream = File.Create("TextFileGenerator.txt"))
            {
                outputStream.Position = outputStream.Length;
                Generator generator = new Generator(options);

                stopwatch.Start();
                generator.Generate(outputStream);
                stopwatch.Stop();
            }

            Console.WriteLine("File generated: " + stopwatch.Elapsed);
            Console.ReadKey(false);
        }
    }
}
