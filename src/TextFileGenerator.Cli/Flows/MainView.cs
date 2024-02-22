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
using System.Reflection;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    internal class MainView
    {
        private const ConsoleColor EnhancedColor = ConsoleColor.Green;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;

        public void WriteHeader()
        {
            Version version = GetVersion();

            Console.WriteLine("TextFileGenerator " + version.ToString(3));
            Console.WriteLine("===============================================================================");
            Console.WriteLine();
        }

        private static Version GetVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version;
        }

        public void DisplayError(Exception ex)
        {
            Console.WriteLine();
            ConsoleWriteLine(ex.ToString(), ErrorColor);
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