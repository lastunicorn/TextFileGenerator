using System;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Parameter
    {
        public string Key { get; set; }

        public IValueProvider ValueProvider { get; set; }

        public string GetValue()
        {
            if (ValueProvider == null)
                throw new Exception("ValueProvider was not set.");

            return ValueProvider.GetValue();
        }
    }
}
