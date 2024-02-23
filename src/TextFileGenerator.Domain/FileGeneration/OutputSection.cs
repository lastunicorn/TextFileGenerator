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
    public class OutputSection
    {
        private readonly Section section;
        private SectionSeparator separator;

        public OutputSection(Section section)
        {
            this.section = section ?? throw new ArgumentNullException(nameof(section));
        }

        public void Serialize(TextWriter textWriter, IEnumerable<Parameter> additionalParameters = null)
        {
            section.Parameters.ResetAll();
            separator = new SectionSeparator(section.Separator);

            List<Parameter> allParameters = RetrieveAllParameters(section.Parameters, additionalParameters).ToList();

            for (int i = 0; i < section.RepeatCount; i++)
            {
                WriteSeparatorBeforeItem(textWriter, i);
                WriteSectionContent(textWriter, allParameters);
                WriteSeparatorAfterItem(textWriter);

                section.Parameters.MoveAllToNextValue();
            }
        }

        private static IEnumerable<Parameter> RetrieveAllParameters(IEnumerable<Parameter> parameters, IEnumerable<Parameter> additionalParameters)
        {
            if (parameters == null)
                return additionalParameters;

            if (additionalParameters == null)
                return parameters;

            return parameters.Concat(additionalParameters);
        }

        private void WriteSeparatorBeforeItem(TextWriter textWriter, int i)
        {
            bool shouldWriteSeparatorBeforeSection = section.SeparatorLocation == SeparatorLocation.Prefix ||
                                                     (section.SeparatorLocation == SeparatorLocation.Infix && i > 0);

            if (shouldWriteSeparatorBeforeSection)
                textWriter.Write(separator);
        }

        private void WriteSectionContent(TextWriter textWriter, IEnumerable<Parameter> allParameters)
        {
            if (section.SectionText != null)
            {
                string text = section.SectionText.Format(allParameters);
                textWriter.Write(text);
            }
            else
            {
                IEnumerable<OutputSection> childOutputSections = section.Sections
                    .Select(x => new OutputSection(x));

                foreach (OutputSection childOutputSection in childOutputSections)
                    childOutputSection.Serialize(textWriter, allParameters);
            }
        }

        private void WriteSeparatorAfterItem(TextWriter textWriter)
        {
            bool shouldWriteSeparatorAfterSection = section.SeparatorLocation == SeparatorLocation.Postfix;

            if (shouldWriteSeparatorAfterSection)
                textWriter.Write(separator);
        }
    }
}