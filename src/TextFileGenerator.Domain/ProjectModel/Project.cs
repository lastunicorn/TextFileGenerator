﻿// TextFileGenerator
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

namespace DustInTheWind.TextFileGenerator.Domain.ProjectModel
{
    public class Project
    {
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public List<Section> Sections { get; } = new List<Section>();

        public List<Parameter> Parameters { get; } = new List<Parameter>();

        public Project()
        {
            Id = Guid.NewGuid();
        }

        public Project(Guid id)
        {
            Id = id;
        }

        public bool ContainsSection(Guid sectionId)
        {
            return Sections.Any(x => x.Id == sectionId || x.ContainsSection(sectionId));
        }

        public Section FindSection(Guid sectionId)
        {
            foreach (Section section in Sections)
            {
                if (section.Id == sectionId)
                    return section;

                Section childSection = section.FindSection(sectionId);

                if (childSection != null)
                    return childSection;
            }

            return null;
        }
    }
}