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

namespace DustInTheWind.TextFileGenerator.Domain.FileGeneration
{
    internal class SectionSeparator
    {
        private readonly string displayValue;

        public SectionSeparator(string value)
        {
            displayValue = ProcessValue(value);
        }

        private static string ProcessValue(string value)
        {
            if (value.IsNullOrEmpty())
                return null;

            List<char> chars = new List<char>(value.Length);

            bool isEscapeMode = false;

            foreach (char c in value)
            {
                if (isEscapeMode)
                {
                    switch (c)
                    {
                        case '\\': chars.Add('\\'); break;
                        case 'r': chars.Add('\r'); break;
                        case 'n': chars.Add('\n'); break;
                        case 't': chars.Add('\t'); break;

                        default:
                            chars.Add('\\');
                            chars.Add(c);
                            break;
                    }

                    isEscapeMode = false;
                }
                else if (c == '\\')
                {
                    isEscapeMode = true;
                }
                else
                {
                    chars.Add(c);
                }
            }

            return string.Join(string.Empty, chars);
        }

        public override string ToString()
        {
            return displayValue;
        }
    }
}