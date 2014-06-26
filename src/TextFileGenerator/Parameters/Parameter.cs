using System;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class Parameter : IParameter
    {
        private readonly string key;
        private readonly IValueProvider valueProvider;

        public string Key
        {
            get { return key; }
        }

        public Parameter(string key, IValueProvider valueProvider)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (valueProvider == null)
                throw new ArgumentNullException("valueProvider");

            this.key = key;
            this.valueProvider = valueProvider;
        }

        public string GetValue()
        {
            return valueProvider.GetValue();
        }
    }
}
