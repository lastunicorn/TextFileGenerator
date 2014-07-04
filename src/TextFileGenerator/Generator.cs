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
using System.IO;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator
{
    public class Generator
    {
        private readonly GeneratorOptions options;

        public Generator(GeneratorOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            this.options = options;
        }

        public void Generate(Stream outputStream)
        {
            TextWriter textWriter = new StreamWriter(outputStream);
            Generate(textWriter);
        }

        public void Generate(TextWriter textWriter)
        {
            foreach (Section section in options.Sections)
            {
                SectionGenerator.WriteSection(textWriter, section, null);
            }

            textWriter.Flush();
        }
    }
}