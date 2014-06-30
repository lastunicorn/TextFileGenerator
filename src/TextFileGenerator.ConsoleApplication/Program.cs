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

            using (Stream inputStream = File.OpenRead("TextFileGenerator.xml"))
            {
                OptionsSerializer serializer = new OptionsSerializer();
                options = serializer.Deserialize(inputStream);
            }

            using (Stream outputStream = File.OpenWrite("TextFileGenerator.txt"))
            {
                Generator generator = new Generator(options);
                generator.Generate(outputStream);
            }
        }
    }
}
