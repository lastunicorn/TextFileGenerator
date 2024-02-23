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

using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace TextFileGenerator.Wpf;

public class ProjectService
{
    public List<Project> RetrieveProjects()
    {
        List<Project> projects = new()
        {
            new Project
            {
                Name = "Text 1000 rows",
                Sections =
                {
                    new Section
                    {
                        Name = "Section 1",
                        Parameters =
                        {
                            new Parameter(),
                            new Parameter(),
                            new Parameter()
                        }
                    },
                    new Section
                    {
                        Name = "Section 2",
                        Parameters =
                        {
                            new Parameter(),
                            new Parameter(),
                            new Parameter(),
                            new Parameter(),
                            new Parameter()
                        }
                    }
                }
            },
            new Project
            {
                Name = "Binary 10 MB"
            },
            new Project
            {
                Name = "XML",
                Sections =
                {
                    new Section
                    {
                        Name = "Section 1",
                        Parameters =
                        {
                            new Parameter()
                        }
                    },
                }
            }
        };

        return projects;
    }
}