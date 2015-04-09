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
using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public class OptionsTranslator
    {
        public static textFileGenerator Translate(FileDescriptor sourceOptions)
        {
            textFileGenerator destinationOptions = new textFileGenerator();

            CreateSections(destinationOptions, sourceOptions.Sections);

            return destinationOptions;
        }

        private static void CreateSections(textFileGenerator destinationOptions, IReadOnlyList<Section> sourceSections)
        {
            if (sourceSections == null || sourceSections.Count == 0)
                return;

            destinationOptions.sections = new section[sourceSections.Count];

            for (int i = 0; i < sourceSections.Count; i++)
            {
                section destinationSection = SectionTranslator.Translate(sourceSections[i]);
                destinationOptions.sections[i] = destinationSection;
            }
        }

        public static FileDescriptor Translate(textFileGenerator sourceOptions)
        {
            FileDescriptor destinationOptions = new FileDescriptor();

            CreateSections(destinationOptions, sourceOptions.sections);

            return destinationOptions;
        }

        private static void CreateSections(FileDescriptor destinationOptions, IEnumerable<section> sourceSections)
        {
            if (sourceSections == null)
                return;

            IEnumerable<Section> destinationSections = sourceSections.Select(SectionTranslator.Translate);
            destinationOptions.Sections.AddRange(destinationSections);
        }
    }
}
