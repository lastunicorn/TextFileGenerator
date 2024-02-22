// VeloCity
// Copyright (C) 2022-2023 Dust in the Wind
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
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    public class UserInterface : IUserInterface
    {
        private const ConsoleColor EnhancedColor = ConsoleColor.Green;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;

        public void DisplayOptionFileReading(string fileName)
        {
            Write("Reading description file");
            WriteEnhanced(fileName);
            Write(" ");
        }

        public void DisplayOk()
        {
            WriteLine("[Done]");
        }

        public void DisplayOutputFileGenerating(string fileName)
        {
            Write("Generating file");
            WriteEnhanced(fileName);
            Write(" ");
        }

        public void DisplayElapsedTime(TimeSpan elapsed)
        {
            WriteLine();
            Write("Elapsed time:");
            WriteLineEnhanced(elapsed.ToString());
        }

        public void DisplayOutputFileGenerateDone(string outputFileName)
        {
            Write("Scaffold file created successfully:");
            WriteEnhanced(outputFileName);
            WriteLine();
        }

        public void ExecuteWithSpinner(Action action)
        {
            SquareDotTemplate spinnerTemplate = new SquareDotTemplate();

            using (ConsoleSpinner consoleSpinner = new ConsoleSpinner(spinnerTemplate))
            {
                consoleSpinner.Start();

                try
                {
                    action();
                }
                finally
                {
                    consoleSpinner.Stop();
                }
            }
        }

        private void Write(string text)
        {
            Console.Write(text);
        }

        private void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        private void WriteLine()
        {
            Console.WriteLine();
        }

        private void WriteEnhanced(string text)
        {
            ConsoleWrite(text, EnhancedColor);
        }

        private void WriteLineEnhanced(string text)
        {
            ConsoleWriteLine(text, EnhancedColor);
        }

        private static void ConsoleWrite(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);

            Console.ForegroundColor = oldColor;
        }

        private static void ConsoleWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = oldColor;
        }
    }
}