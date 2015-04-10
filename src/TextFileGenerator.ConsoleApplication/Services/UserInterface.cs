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
using DustInTheWind.TextFileGenerator.ConsoleApplication.Properties;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Services
{
    class UserInterface
    {
        private const ConsoleColor EnhancedColor = ConsoleColor.Green;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteEnhanced(string text)
        {
            ConsoleWrite(text, EnhancedColor);
        }

        public void WriteLineEnhanced(string text)
        {
            ConsoleWriteLine(text, EnhancedColor);
        }

        public void DisplayError(Exception ex)
        {
            Console.WriteLine();
            ConsoleWriteLine(ex.ToString(), ErrorColor);
        }

        public void Pause()
        {
            Console.WriteLine();
            Console.Write(Resources.UserInterface_PauseMessage);

            Console.ReadKey(false);
        }

        private static void ConsoleWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = oldColor;
        }

        private static void ConsoleWrite(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);

            Console.ForegroundColor = oldColor;
        }
    }
}