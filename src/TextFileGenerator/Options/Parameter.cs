using System;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Parameter
    {
        private IValueProvider valueProvider;
        public string Key { get; set; }

        public IValueProvider ValueProvider
        {
            get { return valueProvider; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                valueProvider = value;
            }
        }

        public Parameter()
        {
            valueProvider = new EmptyValueProvider();
        }

        public string CurrentValue
        {
            get
            {
                return valueProvider.CurrentValue;
            }
        }

        public string NextValue
        {
            get { return valueProvider.MoveToNextValue(); }
        }

        public void MoveToNextValue()
        {
            valueProvider.MoveToNextValue();
        }

        public void Reset()
        {
            valueProvider.Reset();
        }
    }
}
