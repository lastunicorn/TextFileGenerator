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
using System.Text.RegularExpressions;

namespace DustInTheWind.TextFileGenerator.FileDescription
{
    public class SectionText
    {
        //private readonly Regex regex;

        //public SectionText()
        //{
        //    regex = new Regex(@"{([^{}]*)}");
        //}

        public string Value { get; set; }

        public string Format(IEnumerable<Parameter> parameters)
        {
            if (Value == null)
                return string.Empty;

            string text = Value;

            if (parameters != null)
                text = FormatUsingParameters(text, parameters);

            return text;
        }

        //private string FormatUsingParameters(string text, IEnumerable<Parameter> parameters)
        //{
        //    return regex.Replace(text, match =>
        //    {
        //        string value = parameters
        //             .Where(x => x.Name == match.Groups[1].Value)
        //             .Select(x => x.NextValue)
        //             .FirstOrDefault();

        //        return value ?? match.Groups[0].Value;
        //    });
        //}

        private string FormatUsingParameters(string text, IEnumerable<Parameter> parameters)
        {
            foreach (Parameter parameter in parameters)
            {
                string key = FormatParameterKey(parameter);
                string value = parameter.NextValue;

                text = text.Replace(key, value);
            }

            return text;
        }

        //private string FormatUsingParameters(string text, IEnumerable<Parameter> parameters)
        //{


        //    foreach (Parameter parameter in parameters)
        //    {
        //        string key = FormatParameterKey(parameter);
        //        string value = parameter.NextValue;

        //        //StringBuilder sb = new StringBuilder();

        //        //int i1 = 0;
        //        //int i2 = 0;

        //        //while (true)
        //        //{
        //        //    i2 = text.IndexOf(key);

        //        //    if (pos == -1)
        //        //        break;

        //        //    sb.Append(text.Substring())
        //        //}
        //        text = text.Replace(key, value);
        //    }

        //    return text;
        //}

        private static string FormatParameterKey(Parameter parameter)
        {
            return string.Format("{{{0}}}", parameter.Name);
        }
    }
}
