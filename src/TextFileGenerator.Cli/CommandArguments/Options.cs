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

using CommandLine;

namespace DustInTheWind.TextFileGenerator.Cli.CommandArguments
{
    internal class Options
    {
        [Value(0)]
        public IList<string> DescriptorFileNames { get; set; }

        [Option('t', "template", Required = false, Default = false, HelpText = "generates the template file.")]
        public bool GenerateScaffold { get; set; }
    }
}
