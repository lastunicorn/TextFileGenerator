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

using System.Collections.ObjectModel;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace TextFileGenerator.Wpf;

public class MainViewModel : ViewModelBase
{
    private readonly ProjectService projectService;

    public ObservableCollection<ProjectItemViewModel> Projects { get; } = new();

    public MainViewModel(ProjectService projectService)
    {
        this.projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));

        RefreshProjects();
    }

    private void RefreshProjects()
    {
        List<Project> projects = projectService.RetrieveProjects();

        IEnumerable<ProjectItemViewModel> projectItems = projects
            .Select(x => new ProjectItemViewModel
            {
                Label = x.Name,
                Sections = x.Sections
                    .Select(z => new SectionItemViewModel
                    {
                        Label = z.Name,
                        Parameters = (z.Parameters?.Count ?? 0).ToString()
                    })
                    .ToObservableCollection()
            });

        foreach (ProjectItemViewModel projectItemViewModel in projectItems)
            Projects.Add(projectItemViewModel);
    }
}