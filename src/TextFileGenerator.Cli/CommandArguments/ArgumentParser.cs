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
    class ArgumentParser
    {
        private readonly IReadOnlyList<string> args;
        private int index;

        public ArgumentParser(IReadOnlyList<string> args)
        {
            if (args == null) throw new ArgumentNullException("args");
            this.args = args;
        }

        public Argument GetNext()
        {
            if (index == args.Count)
                return null;

            string id;
            string value;

            if (args[index].StartsWith("-"))
            {
                int pos = args[index].IndexOf(':', 1);

                if (pos == -1)
                {
                    id = args[index].Substring(1);
                    value = null;
                }
                else
                {
                    id = args[index].Substring(1, pos);
                    value = args[index].Substring(pos + 1);
                }
            }
            else
            {
                id = null;
                value = args[index];
            }

            index++;

            return new Argument(id, value);
        }
    }
}