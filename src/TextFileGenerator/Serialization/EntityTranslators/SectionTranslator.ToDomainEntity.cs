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
using System.Linq;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.Templating;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public static partial class SectionTranslator
    {
        public static Section ToDomainEntity(section sourceSection)
        {
            Section destinationSection = new Section
            {
                Name = sourceSection.name,
                RepeatCount = int.Parse(sourceSection.repeat),
                Separator = sourceSection.separator,
                SeparatorLocation = Translate(sourceSection.separatorLocation),
            };

            TranslateContent(destinationSection, sourceSection);

            CreateParameters(destinationSection, sourceSection.parameter);

            return destinationSection;
        }

        private static SeparatorLocation Translate(separatorLocation sourceSeparatorType)
        {
            switch (sourceSeparatorType)
            {
                case separatorLocation.Infix:
                    return SeparatorLocation.Infix;

                case separatorLocation.Prefix:
                    return SeparatorLocation.Prefix;

                case separatorLocation.Postfix:
                    return SeparatorLocation.Postfix;

                default:
                    return SeparatorLocation.Infix;
            }
        }

        private static void TranslateContent(Section destinationSection, section sourceSection)
        {
            if (sourceSection.Items == null || sourceSection.Items.Length <= 0)
                return;

            foreach (object item in sourceSection.Items)
            {
                if (item == null)
                    continue;

                Type itemType = item.GetType();

                if (itemType == typeof(string))
                {
                    destinationSection.SectionText = new TextTemplate((string)item);
                }

                if (itemType == typeof(section))
                {
                    Section destinationSubsection = ToDomainEntity((section)item);
                    destinationSection.Sections.Add(destinationSubsection);
                }
            }
        }

        private static void CreateParameters(Section destinationSection, IEnumerable<parameter> sourceParameters)
        {
            if (sourceParameters == null)
                return;

            IEnumerable<Parameter> destinationParameters = sourceParameters.Select(ParameterTranslator.ToDomainEntity);
            destinationSection.Parameters.AddRange(destinationParameters);
        }
    }
}