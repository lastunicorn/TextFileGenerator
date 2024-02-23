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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DustInTheWind.TextFileGenerator.Wpf;

internal class TreeViewBehaviors
{
    public static ICommand GetSelectedItemChangedCommand(TreeView treeView)
    {
        return (ICommand)treeView.GetValue(SelectedItemChangedCommandProperty);
    }

    public static void SetSelectedItemChangedCommand(TreeView treeView, ICommand value)
    {
        treeView.SetValue(SelectedItemChangedCommandProperty, value);
    }

    public static readonly DependencyProperty SelectedItemChangedCommandProperty = DependencyProperty.RegisterAttached(
            "SelectedItemChangedCommand",
            typeof(ICommand),
            typeof(TreeViewBehaviors),
            new UIPropertyMetadata(HandleCommandChanged));

    private static void HandleCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TreeView treeView)
            return;

        if (e.OldValue is ICommand)
        {
            if (e.NewValue is not ICommand)
                treeView.SelectedItemChanged -= HandleSelectedItemChanged;
        }
        else
        {
            if (e.NewValue is ICommand)
                treeView.SelectedItemChanged += HandleSelectedItemChanged;
        }
    }

    private static void HandleSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        // Only react to the Selected event raised by the TreeViewItem
        // whose IsSelected property was modified. Ignore all ancestors
        // who are merely reporting that a descendant's Selected fired.
        if (!ReferenceEquals(sender, e.OriginalSource))
            return;

        if (e.OriginalSource is TreeView treeView)
        {
            ICommand command = GetSelectedItemChangedCommand(treeView);
            command.Execute(e.NewValue);
        }
    }
}