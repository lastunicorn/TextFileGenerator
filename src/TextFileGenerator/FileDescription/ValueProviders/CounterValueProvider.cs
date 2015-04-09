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

using System.Globalization;

namespace DustInTheWind.TextFileGenerator.FileDescription.ValueProviders
{
    public class CounterValueProvider : IValueProvider
    {
        private int currentValue;
        private int startValue;
        private string currentValueAsString;
        private bool isFirst;

        public int StartValue
        {
            get { return startValue; }
            set
            {
                startValue = value;
                currentValue = value;
                isFirst = true;
            }
        }

        public int Step { get; set; }

        public string Format { get; set; }

        public CounterValueProvider()
        {
            StartValue = 1;
            Step = 1;
            isFirst = true;
        }

        public string MoveToNextValue()
        {
            GenerateNextValue();

            return currentValueAsString;
        }

        private void GenerateNextValue()
        {
            if (isFirst)
                isFirst = false;
            else
                currentValue += Step;

            currentValueAsString = Format != null
                ? currentValue.ToString(Format, CultureInfo.CurrentCulture)
                : currentValue.ToString(CultureInfo.CurrentCulture);
        }

        public string CurrentValue
        {
            get { return currentValueAsString; }
        }

        public void Reset()
        {
            currentValue = startValue;
            isFirst = true;
        }
    }
}