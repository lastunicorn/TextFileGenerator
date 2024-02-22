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
using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.CommandArguments
{
    class Arguments
    {
        public string DescriptorFileName { get; private set; }
        public bool GenerateScaffold { get; private set; }

        public Arguments(IReadOnlyList<string> args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            ParseArgs(args);
        }

        private void ParseArgs(IReadOnlyList<string> args)
        {
            ArgumentParser argumentParser = new ArgumentParser(args);

            Argument argument = argumentParser.GetNext();

            if (argument == null)
                return;

            switch (argument.Id)
            {
                case null:
                    DescriptorFileName = argument.Value;
                    break;

                case "x":
                    GenerateScaffold = true;
                    break;
            }
        }
    }
}
