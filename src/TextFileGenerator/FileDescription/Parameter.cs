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
using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;

namespace DustInTheWind.TextFileGenerator.FileDescription
{
    public class Parameter
    {
        private bool isNew;
        private IValueProvider valueProvider;

        public string Name { get; set; }
        public ValueChangeMode ValueChangeMode { get; set; }

        public IValueProvider ValueProvider
        {
            get { return valueProvider; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                valueProvider = value;
            }
        }

        public Parameter()
        {
            valueProvider = EmptyValueProvider.Value;
            isNew = true;
        }

        public string CurrentValue { get; private set; }

        public string NextValue
        {
            get
            {
                if (isNew || ValueChangeMode == ValueChangeMode.Auto)
                {
                    CurrentValue = valueProvider.GetNextValue();
                    isNew = false;
                }

                return CurrentValue;
            }
        }

        public void MoveToNextValue()
        {
            if (ValueChangeMode == ValueChangeMode.Manual)
                CurrentValue = valueProvider.GetNextValue();
        }

        public void Reset()
        {
            valueProvider.Reset();
            isNew = true;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, ValueProvider.GetType().Name);
        }
    }
}
