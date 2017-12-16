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
using System.Linq;
using System.Text;
using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.Templating
{
    public class TextTemplate
    {
        public static TextTemplate Empty { get; }

        static TextTemplate()
        {
            Empty = new TextTemplate(string.Empty);
        }

        public string Value { get; }

        private TemplateItemList templateItems;

        public TextTemplate(string value)
        {
            Value = value;
        }

        public string Format(IEnumerable<Parameter> parameters)
        {
            if (Value == null)
                return string.Empty;

            return parameters == null
                ? Value
                : FormatInternal(parameters);
        }

        private string FormatInternal(IEnumerable<Parameter> parameters)
        {
            if (templateItems == null)
                Analize();

            StringBuilder sb = new StringBuilder();

            foreach (TemplateItem templateItem in templateItems)
            {
                switch (templateItem.Type)
                {
                    case TemplateItemType.Text:
                        sb.Append(templateItem.Text);
                        break;

                    case TemplateItemType.Parameter:
                        string parameterName = templateItem.Text;
                        Parameter parameter = parameters.FirstOrDefault(x => x.Name == parameterName);

                        string value = parameter == null
                            ? "{" + parameterName + "}"
                            : parameter.NextValue;

                        sb.Append(value);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return sb.ToString();
        }

        private void Analize()
        {
            templateItems = new TemplateItemList();
            string text = Value;
            int startIndex = 0;

            do
            {
                int pos1 = text.IndexOf('{', startIndex);

                if (pos1 == -1)
                {
                    templateItems.AddTextPart(text.Substring(startIndex));
                    startIndex = -1;
                }
                else
                {
                    templateItems.AddTextPart(text.Substring(startIndex, pos1 - startIndex));

                    int pos2 = text.IndexOfAny(new[] { '{', '}' }, pos1 + 1);

                    if (pos2 == -1)
                    {
                        templateItems.AddTextPart(text.Substring(pos1));
                        startIndex = -1;
                    }
                    else if (text[pos2] == '{')
                    {
                        templateItems.AddTextPart(text.Substring(pos1, pos2 - pos1));
                        startIndex = pos2;
                    }
                    else if (text[pos2] == '}')
                    {
                        string parameterName = text.Substring(pos1 + 1, pos2 - pos1 - 1);
                        templateItems.AddParameterPart(parameterName);

                        startIndex = pos2 + 1;
                    }
                }
            } while (startIndex != -1);
        }
    }
}
