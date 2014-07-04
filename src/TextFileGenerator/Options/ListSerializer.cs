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
using System.IO;

namespace DustInTheWind.TextFileGenerator.Options
{
    internal class ListSerializer<T>
    {
        private readonly IList<T> list;

        public string Separator { get; set; }
        public SeparatorType SeparatorType { get; set; }

        public Func<T, string> ItemSerializer { get; set; }

        public ListSerializer(IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            this.list = list;
        }

        public void Serialize(TextWriter textWriter)
        {
            bool existsSeparator = !string.IsNullOrEmpty(Separator);

            for (int i = 0; i < list.Count; i++)
            {
                if (existsSeparator)
                    WriteSeparatorBeforeItem(textWriter, Separator, SeparatorType, i);

                textWriter.Write(ItemSerializer(list[i]));

                if (existsSeparator)
                    WriteSeparatorAfterItem(textWriter, Separator, SeparatorType);
            }
        }

        private static void WriteSeparatorAfterItem(TextWriter textWriter, string separator, SeparatorType separatorType)
        {
            bool shouldWriteSeparatorAfterSection = separatorType == SeparatorType.Postfix;

            if (shouldWriteSeparatorAfterSection)
                textWriter.Write(separator);
        }

        private static void WriteSeparatorBeforeItem(TextWriter textWriter, string separator, SeparatorType separatorType, int i)
        {
            bool shouldWriteSeparatorBeforeSection = separatorType == SeparatorType.Prefix ||
                                                      (separatorType == SeparatorType.Infix && i > 0);

            if (shouldWriteSeparatorBeforeSection)
                textWriter.Write(separator);
        }
    }
}
