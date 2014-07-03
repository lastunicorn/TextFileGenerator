using System;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Parameter
    {
        public string Key { get; set; }

        public IValueProvider ValueProvider { get; set; }

        public string GetNextValue()
        {
            if (ValueProvider == null)
                throw new Exception("ValueProvider was not set.");

            return ValueProvider.GetNextValue();
        }

        public string GetCurrentValue()
        {
            if (ValueProvider == null)
                throw new Exception("ValueProvider was not set.");

            return ValueProvider.GetCurrentValue();
        }

        public void Reset()
        {
            ValueProvider.Reset();
        }
    }
}
