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
using DustInTheWind.TextFileGenerator.Templating;

namespace DustInTheWind.TextFileGenerator.FileDescription
{
    public class Section
    {
        public string Name { get; set; }
    
        public TextTemplate SectionText { get; set; }
        
        public int RepeatCount { get; set; } = 1;

        public string Separator { get; set; }
        
        public SeparatorLocation SeparatorLocation { get; set; }
        
        public ParameterList Parameters { get; } = new ParameterList();

        public List<Section> Sections { get; } = new List<Section>();
    }
}