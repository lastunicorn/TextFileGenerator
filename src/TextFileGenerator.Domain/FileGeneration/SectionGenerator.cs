// VeloCity
// Copyright (C) 2022-2023 Dust in the Wind
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
    public class SectionGenerator
    {
        private readonly TextWriter textWriter;

        public SectionGenerator(TextWriter textWriter)
        {
            this.textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        public void WriteSection(Section section, IEnumerable<Parameter> additionalParameters = null)
        {
            section.Parameters.ResetAll();

            bool existsSeparator = !string.IsNullOrEmpty(section.Separator);
            IEnumerable<Parameter> allParameters = ConcatenateAdditionalParameters(section.Parameters, additionalParameters);

            for (int i = 0; i < section.RepeatCount; i++)
            {
                if (existsSeparator)
                    WriteSeparatorBeforeItem(section.Separator, section.SeparatorLocation, i);

                WriteSectionContent(section, allParameters);

                if (existsSeparator)
                    WriteSeparatorAfterItem(section.Separator, section.SeparatorLocation);

                section.Parameters.MoveAllToNextValue();
            }
        }

        private void WriteSectionContent(Section section, IEnumerable<Parameter> allParameters)
        {
            if (section.SectionText != null)
                WriteSectionText(section.SectionText, allParameters);
            else
                WriteSubsections(section.Sections, allParameters);
        }

        private void WriteSectionText(TextTemplate sectionText, IEnumerable<Parameter> parameters)
        {
            string text = sectionText.Format(parameters);
            textWriter.Write(text);
        }

        private void WriteSubsections(IEnumerable<Section> sections, IEnumerable<Parameter> parameters)
        {
            foreach (Section subSection in sections)
                WriteSection(subSection, parameters);
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