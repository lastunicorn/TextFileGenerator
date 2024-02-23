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

namespace DustInTheWind.TextFileGenerator.Wpf.Application;

public class ApplicationState
{
    public List<Project> OpenedProjects { get; } = new();

    public Project CurrentProject { get; set; }

    public ApplicationState()
    {
        OpenedProjects.AddRange(CreateProjects());
    }

    private List<Project> CreateProjects()
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
                    }
                }
            }
        };

        return projects;
    }
}