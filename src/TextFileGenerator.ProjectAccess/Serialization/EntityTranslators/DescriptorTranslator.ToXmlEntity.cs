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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.EntityTranslators
{
    public static partial class DescriptorTranslator
    {
        public static textFileGenerator ToXmlEntity(Project project)
        {
            textFileGenerator result = new textFileGenerator();

            if (project.Sections != null && project.Sections.Count > 0)
                CreateSections(result, project.Sections);

            return result;
        }

        private static void CreateSections(textFileGenerator result, IReadOnlyList<Section> sourceSections)
        {
            result.section = new section[sourceSections.Count];

            for (int i = 0; i < sourceSections.Count; i++)
            {
                section destinationSection = SectionTranslator.ToXmlEntity(sourceSections[i]);
                result.section[i] = destinationSection;
            }
        }
    }
}
