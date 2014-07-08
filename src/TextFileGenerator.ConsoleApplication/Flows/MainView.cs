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
    class MainView
    {
        public void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");

            Console.ReadKey(false);
        }

        public void DisplayError(Exception ex)
        {
            Console.WriteLine();
            ConsoleWriteLine(ex.ToString(), ConsoleColor.Red);
        }

        public void WriteHeader()
        {
            Console.WriteLine("TextFileGenerator v1.0");
            Console.WriteLine("===============================================================================");
            Console.WriteLine();
        }

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
    }
}