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

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Services
{
    class Arguments
    {
        private readonly string[] args;

        public Arguments(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            this.args = args;
        }

        public string OptionsFileName
        {
            get { return args.Length > 0 ? args[0] : null; }
        }
    }
}
