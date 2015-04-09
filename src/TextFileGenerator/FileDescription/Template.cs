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

namespace DustInTheWind.TextFileGenerator.FileDescription
{
    public class Template
    {
        public string Value { get; set; }

        public string Format(IEnumerable<Parameter> parameters, IEnumerable<Parameter> additionalParameters)
        {
            if (Value == null)
                return string.Empty;

            string text = Value;

            if (parameters != null)
                text = FormatUsingParameters(text, parameters);

            if (additionalParameters != null)
                text = FormatUsingAdditionalParameters(text, additionalParameters);

            return text;
        }

        private static string FormatUsingParameters(string text, IEnumerable<Parameter> parameters)
        {
            foreach (Parameter parameter in parameters)
            {
                string key = FormatParameterKey(parameter);
                string value = parameter.NextValue;

                text = text.Replace(key, value);
            }

            return text;
        }

        private static string FormatUsingAdditionalParameters(string text, IEnumerable<Parameter> additionalParameters)
        {
            foreach (Parameter parameter in additionalParameters)
            {
                string key = FormatParameterKey(parameter);
                string value = parameter.CurrentValue;

                text = text.Replace(key, value);
            }

            return text;
        }

        private static string FormatParameterKey(Parameter parameter)
        {
            return "{" + parameter.Key + "}";
        }
    }
}
