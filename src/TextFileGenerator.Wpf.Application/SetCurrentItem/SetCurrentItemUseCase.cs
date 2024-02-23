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

namespace DustInTheWind.TextFileGenerator.Wpf.Application.SetCurrentItem;

internal class SetCurrentItemUseCase : IRequestHandler<SetCurrentItemRequest>
{
    private readonly ApplicationState applicationState;

    public SetCurrentItemUseCase(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    public Task Handle(SetCurrentItemRequest request, CancellationToken cancellationToken)
    {
        if (request.SectionId.HasValue)
        {
            Section section = applicationState.OpenedProjects
                .Select(x => x.FindSection(request.SectionId.Value))
                .FirstOrDefault(x => x != null);

            if (section != null)
                applicationState.CurrentSection = section;
        }
        else if (request.ProjectId.HasValue)
        {
            Project project = applicationState.OpenedProjects.FirstOrDefault(x => x.Id == request.ProjectId);

            if (project != null)
                applicationState.CurrentProject = project;
        }

        return Task.CompletedTask;
    }
}