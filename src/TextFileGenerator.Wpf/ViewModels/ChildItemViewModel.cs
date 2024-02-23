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
using DustInTheWind.TextFileGenerator.Wpf.Application;
using DustInTheWind.TextFileGenerator.Wpf.Application.PresentProjects;

namespace DustInTheWind.TextFileGenerator.Wpf.ViewModels;

public class ChildItemViewModel
{
    public string Label { get; }

    public Guid Value { get; }

    public ProjectItemType ProjectItemType { get; }

    public ObservableCollection<ChildItemViewModel> Children { get; }

    public ChildItemViewModel(ParameterResponseDto parameterResponseDto)
    {
        Label = parameterResponseDto.Name;
        Value = parameterResponseDto.Id;
        ProjectItemType = ProjectItemType.Parameter;
        Children = new ObservableCollection<ChildItemViewModel>();
    }

    public ChildItemViewModel(SectionResponseDto sectionResponseDto)
    {
        Label = sectionResponseDto.Name;
        Value = sectionResponseDto.Id;
        ProjectItemType = ProjectItemType.Section;

        IEnumerable<ChildItemViewModel> childSections = sectionResponseDto.Sections
            .Select(x => new ChildItemViewModel(x));

        IEnumerable<ChildItemViewModel> childParameters = sectionResponseDto.Parameters
            .Select(x => new ChildItemViewModel(x));

        Children = childSections
            .Concat(childParameters)
            .ToObservableCollection();
    }
}