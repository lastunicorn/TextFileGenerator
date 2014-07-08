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

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    class GenerateTextFileView
    {
        public void WriteUsageHelp()
        {
            Console.WriteLine("Usage: TextFileGenerator.exe <optionsFileName>");
        }

        private void ConsoleWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = oldColor;
        }

        private void ConsoleWrite(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);

            Console.ForegroundColor = oldColor;
        }

        public void DisplayOptionFileReading(string optionsFileName)
        {
            Console.Write("Reading options from ");
            ConsoleWrite(optionsFileName, ConsoleColor.Green);
            Console.Write("... ");
        }

        public void DisplayOptionFileReadingDone()
        {
            Console.WriteLine("Done!");
        }

        public void DisplayOutputFileGenerating(string outputFileName)
        {
            Console.Write("Generating file ");
            ConsoleWrite(outputFileName, ConsoleColor.Green);
            Console.Write("... ");
        }

        public void DisplayOutputFileGenerateDone()
        {
            Console.WriteLine("Done!");
        }

        public void DisplayElapsedTime(TimeSpan elapsed)
        {
            Console.WriteLine();
            Console.Write("Elapsed time: ");
            ConsoleWriteLine(elapsed.ToString(), ConsoleColor.Green);
        }
    }
}
