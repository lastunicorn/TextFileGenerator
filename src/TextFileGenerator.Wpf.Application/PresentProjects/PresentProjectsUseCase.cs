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
using MediatR;

namespace DustInTheWind.TextFileGenerator.Wpf.Application.PresentProjects;

internal class PresentProjectsUseCase : IRequestHandler<PresentProjectsRequest, PresentProjectsResponse>
{
    private readonly ApplicationState applicationState;

    public PresentProjectsUseCase(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    public Task<PresentProjectsResponse> Handle(PresentProjectsRequest request, CancellationToken cancellationToken)
    {
        if (applicationState.OpenedProjects.Count == 0)
            LoadProjects();

        PresentProjectsResponse response = new()
        {
            Projects = applicationState.OpenedProjects
                .Select(x => new ProjectResponseDto(x))
                .ToList()
        };

        return Task.FromResult(response);
    }

    private void LoadProjects()
    {
        List<Project> projects = CreateProjects();
        applicationState.OpenedProjects.AddRange(projects);
    }

    private static List<Project> CreateProjects()
    {
        List<Project> projects = new()
        {
            CreateProject1(),
            CreateProject2(),
            CreateProject3()
        };

        return projects;
    }

    private static Project CreateProject1()
    {
        Project project1 = new()
        {
            Name = "Text 1000 rows"
        };

        project1.Sections.Add(new Section
        {
            Name = "Section 1",
            Parameters =
            {
                new Parameter
                {
                    Name = "Param 1"
                },
                new Parameter
                {
                    Name = "Param 2"
                },
                new Parameter
                {
                    Name = "Param 3"
                }
            }
        });

        project1.Sections.Add(new Section
        {
            Name = "Section 2",
            Parameters =
            {
                new Parameter
                {
                    Name = "Param 1"
                },
                new Parameter
                {
                    Name = "Param 2"
                },
                new Parameter
                {
                    Name = "Param 3"
                },
                new Parameter
                {
                    Name = "Param 4"
                }
            },
            Sections =
            {
                new Section
                {
                    Name = "Section 2-1"
                },
                new Section
                {
                    Name = "Section 2-2"
                }
            }
        });

        project1.Parameters.Add(new Parameter
        {
            Name = "Param 1"
        });

        return project1;
    }

    private static Project CreateProject2()
    {
        Project project2 = new()
        {
            Name = "Binary 10 MB"
        };

        return project2;
    }

    private static Project CreateProject3()
    {
        Project project3 = new()
        {
            Name = "XML"
        };

        project3.Sections.Add(new Section
        {
            Name = "Section 1",
            Parameters =
            {
                new Parameter
                {
                    Name = "Param 1"
                }
            }
        });

        return project3;
    }
}