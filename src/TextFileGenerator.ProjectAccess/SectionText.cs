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

using System.Collections.Generic;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace DustInTheWind.TextFileGenerator.ProjectAccess
{
    public class SectionText
    {
        private TextTemplate textTemplate = TextTemplate.Empty;

        public string Value
        {
            get => textTemplate.Value;
            set => textTemplate = new TextTemplate(value);
        }

        public string Format(IEnumerable<Parameter> parameters)
        {
            return textTemplate.Format(parameters);
        }
    }
}