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
using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.FileGeneration
{
    public class SectionGenerator
    {
        private readonly TextWriter textWriter;

        public SectionGenerator(TextWriter textWriter)
        {
            if (textWriter == null) throw new ArgumentNullException("textWriter");

            this.textWriter = textWriter;
        }

        public void WriteSection(Section section, IEnumerable<Parameter> additionalParameters = null)
        {
            bool existsSeparator = !string.IsNullOrEmpty(section.Separator);

            section.Parameters.ResetAll();

            for (int i = 0; i < section.RepeatCount; i++)
            {
                if (existsSeparator)
                    WriteSeparatorBeforeItem(section.Separator, section.SeparatorLocation, i);

                WriteSectionContent(section, additionalParameters);

                if (existsSeparator)
                    WriteSeparatorAfterItem(section.Separator, section.SeparatorLocation);
            }
        }

        private void WriteSectionContent(Section section, IEnumerable<Parameter> additionalParameters)
        {
            if (section.SectionText != null)
                WriteSectionText(section, additionalParameters);
            else
                WriteSubsections(section, additionalParameters);
        }

        private void WriteSectionText(Section section, IEnumerable<Parameter> additionalParameters)
        {
            string text = section.SectionText.Format(section.Parameters, additionalParameters);
            textWriter.Write(text);
        }

        private void WriteSubsections(Section section, IEnumerable<Parameter> additionalParameters)
        {
            section.Parameters.MoveAllToNextValue();

            IEnumerable<Parameter> calculatedAdditionalParameters = ConcatenateAdditionalParameters(section.Parameters, additionalParameters);

            foreach (Section subSection in section.Sections)
                WriteSection(subSection, calculatedAdditionalParameters);
        }

        private static IEnumerable<Parameter> ConcatenateAdditionalParameters(IEnumerable<Parameter> parameters, IEnumerable<Parameter> additionalParameters)
        {
            if (parameters == null)
                return additionalParameters;

            if (additionalParameters == null)
                return parameters;

            return parameters.Concat(additionalParameters);
        }

        private void WriteSeparatorAfterItem(string separator, SeparatorLocation separatorLocation)
        {
            bool shouldWriteSeparatorAfterSection = separatorLocation == SeparatorLocation.Postfix;

            if (shouldWriteSeparatorAfterSection)
                textWriter.Write(separator);
        }

        private void WriteSeparatorBeforeItem(string separator, SeparatorLocation separatorLocation, int i)
        {
            bool shouldWriteSeparatorBeforeSection = separatorLocation == SeparatorLocation.Prefix ||
                                                     (separatorLocation == SeparatorLocation.Infix && i > 0);

            if (shouldWriteSeparatorBeforeSection)
                textWriter.Write(separator);
        }
    }
}