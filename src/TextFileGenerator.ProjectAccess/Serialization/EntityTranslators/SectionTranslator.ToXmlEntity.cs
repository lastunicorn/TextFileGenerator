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
using System.Globalization;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using XSection = DustInTheWind.TextFileGenerator.Serialization.Section;
using XSeparatorLocation = DustInTheWind.TextFileGenerator.Serialization.SeparatorLocation;
using XParameter = DustInTheWind.TextFileGenerator.Serialization.Parameter;

namespace DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.EntityTranslators
{
    public static partial class SectionTranslator
    {
        public static XSection ToXmlEntity(this Section sourceSection)
        {
            XSection destinationSection = new XSection
            {
                Name = sourceSection.Name,
                Repeat = sourceSection.RepeatCount.ToString(CultureInfo.InvariantCulture),
                Separator = sourceSection.Separator,
                SeparatorLocation = Translate(sourceSection.SeparatorLocation)
            };

            TranslateContent(destinationSection, sourceSection);
            CreateParameters(destinationSection, sourceSection.Parameters);

            return destinationSection;
        }

        private static void TranslateContent(XSection destinationSection, Section sourceSection)
        {
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
                    destinationSection.Items[i] = sourceSection.Sections[i].ToXmlEntity();
            }
        }

        private static XSeparatorLocation Translate(SeparatorLocation value)
        {
            switch (value)
            {
                default:
                case SeparatorLocation.Infix:
                    return XSeparatorLocation.Infix;

                case SeparatorLocation.Prefix:
                    return XSeparatorLocation.Prefix;

                case SeparatorLocation.Postfix:
                    return XSeparatorLocation.Postfix;
            }
        }

        private static void CreateParameters(XSection destinationSection, IList<Parameter> sourceParameters)
        {
            if (sourceParameters.Count == 0)
                return;

            destinationSection.Parameter = new XParameter[sourceParameters.Count];

            for (int i = 0; i < sourceParameters.Count; i++)
            {
                Parameter sourceParameter = sourceParameters[i];
                XParameter destinationParameter = sourceParameter.ToXmlEntity();

                destinationSection.Parameter[i] = destinationParameter;
            }
        }
    }
}