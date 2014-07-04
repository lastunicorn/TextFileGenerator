// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
            WriteHeader();

            try
            {
                ValidateArguments(args);

                string optionsFileName = args[0];
                GeneratorOptions options = ReadOptionsFile(optionsFileName);

                string outputFileName = GenerateOutputFileName(optionsFileName);
                GenerateFile(outputFileName, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ConsoleWrite(ex.ToString(), ConsoleColor.Red);
            }

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");

            Console.ReadKey(false);
        }

        private static void ValidateArguments(string[] args)
        {
            if (args.Length == 0)
            {
                WriteUsageHelp();
                throw new Exception("Error: insufficient parameters.");
            }
        }

        private static string GenerateOutputFileName(string optionsFileName)
        {
            string directoryPath = Path.GetDirectoryName(optionsFileName);
            string outputFileName = Path.GetFileNameWithoutExtension(optionsFileName) + ".output.txt";

            return Path.Combine(directoryPath, outputFileName);
        }

        private static void WriteUsageHelp()
        {
            Console.WriteLine("Usage: TextFileGenerator.exe <optionsFileName>");
        }

        private static void WriteHeader()
        {
            Console.WriteLine("TextFileGenerator v1.0");
            Console.WriteLine("===============================================================================");
            Console.WriteLine();
        }

        private static void ConsoleWrite(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = oldColor;
        }

        private static GeneratorOptions ReadOptionsFile(string optionsFileName)
        {
            GeneratorOptions options;

            Console.Write("Reading options from " + optionsFileName + " ... ");

            using (Stream inputStream = File.OpenRead(optionsFileName))
            {
                OptionsSerializer serializer = new OptionsSerializer();
                options = serializer.Deserialize(inputStream);
            }

            Console.WriteLine("Done!");

            return options;
        }

        private static void GenerateFile(string outputFileName, GeneratorOptions options)
        {
            Stopwatch stopwatch = new Stopwatch();

            Console.Write("Generating file " + outputFileName + "... ");

            Generator generator = new Generator(options);

            using (Stream outputStream = File.Create(outputFileName))
            {
                stopwatch.Start();
                generator.Generate(outputStream);
                stopwatch.Stop();
            }

            Console.WriteLine("Done!");

            Console.WriteLine("File generated: " + stopwatch.Elapsed);
        }
    }
}
