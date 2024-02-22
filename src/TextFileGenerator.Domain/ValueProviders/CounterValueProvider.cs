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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace DustInTheWind.TextFileGenerator.ValueProviders
{
    public class CounterValueProvider : IValueProvider
    {
        private int currentNumber;
        private int startValue;
        private bool isFirst;

        public int StartValue
        {
            get => startValue;
            set
            {
                startValue = value;
                currentNumber = value;
                isFirst = true;
            }
        }

        public int Step { get; set; }
        public string Format { get; set; }
        private string currentValue;

        public CounterValueProvider()
        {
            StartValue = 1;
            Step = 1;
            isFirst = true;
        }

        public string GetNextValue()
        {
            GenerateNextValue();
            return currentValue;
        }

        private void GenerateNextValue()
        {
            if (isFirst)
                isFirst = false;
            else
                currentNumber += Step;

            currentValue = Format != null
                ? currentNumber.ToString(Format, CultureInfo.CurrentCulture)
                : currentNumber.ToString(CultureInfo.CurrentCulture);
        }

        public void Reset()
        {
            currentNumber = startValue;
            isFirst = true;
        }
    }
}