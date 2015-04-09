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
using System.Globalization;
using System.Linq;
using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public class SectionTranslator
    {
        public static section Translate(Section sourceSection)
        {
            section destinationSection = new section
            {
                name = sourceSection.Name,
                repeat = sourceSection.RepeatCount.ToString(CultureInfo.InvariantCulture),
                separator = sourceSection.Separator,
                separatorLocation = Translate(sourceSection.SeparatorLocation)
            };

            if (sourceSection.SectionText != null)
            {
                destinationSection.Items = new object[]
                {
                    sourceSection.SectionText.Value
                };
            }
            else if (sourceSection.Sections.Count > 0)
            {
                destinationSection.Items = new object[sourceSection.Sections.Count];

                for (int i = 0; i < sourceSection.Sections.Count; i++)
                {
                    destinationSection.Items[i] = Translate(sourceSection.Sections[i]);
                }
            }

            CreateParameters(destinationSection, sourceSection.Parameters);

            return destinationSection;
        }

        private static separatorLocation Translate(FileDescription.SeparatorLocation value)
        {
            switch (value)
            {
                default:
                case FileDescription.SeparatorLocation.Infix:
                    return separatorLocation.Infix;

                case FileDescription.SeparatorLocation.Prefix:
                    return separatorLocation.Prefix;

                case FileDescription.SeparatorLocation.Postfix:
                    return separatorLocation.Postfix;
            }
        }

        private static void CreateParameters(section destinationSection, IList<Parameter> sourceParameters)
        {
            if (sourceParameters.Count == 0)
                return;

            destinationSection.parameter = new parameter[sourceParameters.Count];

            for (int i = 0; i < sourceParameters.Count; i++)
            {
                Parameter sourceParameter = sourceParameters[i];
                parameter destinationParameter = ParameterTranslator.CreateParameter(sourceParameter);

                destinationSection.parameter[i] = destinationParameter;
            }
        }

        public static Section Translate(section sourceSection)
        {
            Section destinationSection = new Section
            {
                Name = sourceSection.name,
                RepeatCount = int.Parse(sourceSection.repeat),
                Separator = sourceSection.separator,
                SeparatorLocation = Translate(sourceSection.separatorLocation),
            };

            if (sourceSection.Items != null && sourceSection.Items.Length > 0)
            {
                foreach (object item in sourceSection.Items)
                {
                    if (item == null)
                        continue;

                    Type itemType = item.GetType();

                    if (itemType == typeof(string))
                        destinationSection.SectionText = new SectionText
                        {
                            Value = (string)item
                        };

                    if (itemType == typeof(section))
                    {
                        Section destinationSubsection = Translate((section)item);
                        destinationSection.Sections.Add(destinationSubsection);
                    }
                }
            }

            CreateParameters(destinationSection, sourceSection.parameter);

            return destinationSection;
        }

        private static void CreateParameters(Section destinationSection, IEnumerable<parameter> sourceParameters)
        {
            if (sourceParameters == null)
                return;

            IEnumerable<Parameter> destinationParameters = sourceParameters.Select(ParameterTranslator.CreateParameter);
            destinationSection.Parameters.AddRange(destinationParameters);
        }

        private static FileDescription.SeparatorLocation Translate(separatorLocation sourceSeparatorType)
        {
            switch (sourceSeparatorType)
            {
                case separatorLocation.Infix:
                    return FileDescription.SeparatorLocation.Infix;

                case separatorLocation.Prefix:
                    return FileDescription.SeparatorLocation.Prefix;

                case separatorLocation.Postfix:
                    return FileDescription.SeparatorLocation.Postfix;

                default:
                    return FileDescription.SeparatorLocation.Infix;
            }
        }
    }
}