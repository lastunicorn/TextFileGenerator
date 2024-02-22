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
using System.Linq;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.EntityTranslators
{
    public static partial class DescriptorTranslator
    {
        public static Project ToDomainEntity(textFileGenerator source)
        {
            Project project = new Project();

            if (source.section != null)
                CreateSections(project, source.section);

            return project;
        }

        private static void CreateSections(Project project, IEnumerable<section> sourceSections)
        {
            IEnumerable<Section> destinationSections = sourceSections.Select(SectionTranslator.ToDomainEntity);
            project.Sections.AddRange(destinationSections);
        }
    }
}