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
using System.IO;
using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.FileGeneration
{
    public class Generator
    {
        private readonly FileDescriptor fileDescriptor;

        public Generator(FileDescriptor fileDescriptor)
        {
            if (fileDescriptor == null) throw new ArgumentNullException(nameof(fileDescriptor));

            this.fileDescriptor = fileDescriptor;
        }

        public void Generate(Stream outputStream)
        {
            TextWriter textWriter = new StreamWriter(outputStream);
            Generate(textWriter);
        }

        private void Generate(TextWriter textWriter)
        {
            SectionGenerator sectionGenerator = new SectionGenerator(textWriter);

            foreach (Section section in fileDescriptor.Sections)
                sectionGenerator.WriteSection(section);

            textWriter.Flush();
        }
    }
}