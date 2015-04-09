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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.FileGeneration
{
    public class SectionGenerator
    {
        public static void WriteSection(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            bool existsSeparator = !string.IsNullOrEmpty(section.Separator);

            foreach (Parameter parameter in section.Parameters)
            {
                parameter.Reset();
            }

            for (int i = 0; i < section.RepeatCount; i++)
            {
                if (existsSeparator)
                    WriteSeparatorBeforeItem(textWriter, section.Separator, section.SeparatorLocation, i);

                WriteSectionContent(textWriter, section, additionalParameters);

                if (existsSeparator)
                    WriteSeparatorAfterItem(textWriter, section.Separator, section.SeparatorLocation);
            }
        }

        private static void WriteSectionContent(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            if (section.SectionText != null)
                WriteSectionTemplate(textWriter, section, additionalParameters);
            else
                WriteSubsections(textWriter, section, additionalParameters);
        }

        private static void WriteSectionTemplate(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            string text = section.SectionText.Format(section.Parameters, additionalParameters);
            textWriter.Write(text);
        }

        private static void WriteSubsections(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            section.Parameters.MoveAllToNextValue();

            IEnumerable<Parameter> calculatedAdditionalParameters = ConcatenateAdditionalParameters(section.Parameters, additionalParameters);

            foreach (Section subSection in section.Sections)
            {
                WriteSection(textWriter, subSection, calculatedAdditionalParameters);
            }
        }

        private static IEnumerable<Parameter> ConcatenateAdditionalParameters(IEnumerable<Parameter> parameters, IEnumerable<Parameter> additionalParameters)
        {
            if (parameters == null)
                return additionalParameters;

            if (additionalParameters == null)
                return parameters;

            return parameters.Concat(additionalParameters);
        }

        private static void WriteSeparatorAfterItem(TextWriter textWriter, string separator, SeparatorLocation separatorLocation)
        {
            bool shouldWriteSeparatorAfterSection = separatorLocation == SeparatorLocation.Postfix;

            if (shouldWriteSeparatorAfterSection)
                textWriter.Write(separator);
        }

        private static void WriteSeparatorBeforeItem(TextWriter textWriter, string separator, SeparatorLocation separatorLocation, int i)
        {
            bool shouldWriteSeparatorBeforeSection = separatorLocation == SeparatorLocation.Prefix ||
                                                     (separatorLocation == SeparatorLocation.Infix && i > 0);

            if (shouldWriteSeparatorBeforeSection)
                textWriter.Write(separator);
        }
    }
}