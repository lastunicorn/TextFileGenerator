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

using System;
using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.Templating
{
    internal class TemplateItemList : List<TemplateItem>
    {
        public void AddTextPart(string text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            if (Count > 0)
            {
                TemplateItem lastPart = this[Count - 1];

                if (lastPart.Type == TemplateItemType.Text)
                {
                    RemoveAt(Count - 1);
                    text = lastPart.Text + text;
                }
            }

            TemplateItem templateItem = new TemplateItem(text, TemplateItemType.Text);
            Add(templateItem);
        }

        public void AddParameterPart(string parameterName)
        {
            TemplateItem templateItem = new TemplateItem(parameterName, TemplateItemType.Parameter);
            Add(templateItem);
        }
    }
}