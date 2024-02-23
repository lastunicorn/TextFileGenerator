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

using System.Collections.ObjectModel;
using DustInTheWind.TextFileGenerator.Wpf.Application.PresentProjects;

namespace DustInTheWind.TextFileGenerator.Wpf.ViewModels;

public class ProjectItemViewModel : ViewModelBase
{
    private bool isChanged;
    private string label;

    public string Label
    {
        get => label;
        set
        {
            if (value == label)
                return;

            label = value;
            OnPropertyChanged();
        }
    }

    public ProjectResponseDto Value { get; }

    public bool IsChanged
    {
        get => isChanged;
        set
        {
            if (value == isChanged)
                return;

            isChanged = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ChildItemViewModel> Children { get; }

    public ProjectItemViewModel(ProjectResponseDto projectResponseDto)
    {
        Label = projectResponseDto.Name;
        Value = projectResponseDto;

        Children = projectResponseDto.Sections
            .Select(z => new ChildItemViewModel(z))
            .ToObservableCollection();

        IEnumerable<ChildItemViewModel> childSections = projectResponseDto.Sections
            .Select(x => new ChildItemViewModel(x));

        IEnumerable<ChildItemViewModel> childParameters = projectResponseDto.Parameters
            .Select(x => new ChildItemViewModel(x));

        Children = childSections
            .Concat(childParameters)
            .ToObservableCollection();
    }
}