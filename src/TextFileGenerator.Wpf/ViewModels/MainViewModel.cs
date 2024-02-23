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
using DustInTheWind.TextFileGenerator.Wpf.Application.PresentProjects;
using MediatR;

namespace DustInTheWind.TextFileGenerator.Wpf;

public class MainViewModel : ViewModelBase
{
    private readonly IMediator mediator;

    public ObservableCollection<ProjectItemViewModel> Projects { get; } = new();

    public MainViewModel(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        _ = RefreshProjects();
    }

    private async Task RefreshProjects()
    {
        PresentProjectsRequest request = new();
        PresentProjectsResponse response = await mediator.Send(request);
        List<ProjectResponseDto> projects = response.Projects;

        IEnumerable<ProjectItemViewModel> projectItemViewModels = projects
            .Select(x => new ProjectItemViewModel(x));

        foreach (ProjectItemViewModel projectItemViewModel in projectItemViewModels)
            Projects.Add(projectItemViewModel);
    }
}