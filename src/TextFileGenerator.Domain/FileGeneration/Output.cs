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
using System.Linq;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace DustInTheWind.TextFileGenerator.Domain.FileGeneration
{
    public class Output : IDisposable
    {
        private readonly TextWriter textWriter;

        public Output(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            textWriter = new StreamWriter(stream);
        }

        public Output(TextWriter textWriter)
        {
            this.textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        public void AddSections(IEnumerable<Section> sections)
        {
            IEnumerable<OutputSection> outputSections = sections
                .Select(x => new OutputSection(x));

            foreach (OutputSection outputSection in outputSections)
                outputSection.Serialize(textWriter);

            textWriter.Flush();
        }

        public void AddSection(Section section)
        {
            OutputSection outputSection = new OutputSection(section);
            outputSection.Serialize(textWriter);

            textWriter.Flush();
        }

        public void Flush()
        {
            textWriter.Flush();
        }

        public void Dispose()
        {
            textWriter?.Dispose();
        }
    }
}