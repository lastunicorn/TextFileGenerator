using System.Globalization;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class CounterParameter : IParameter
    {
        private int nextValue;
        private int startValue;

        public string Key { get; set; }

        public int StartValue
        {
            get { return startValue; }
            set
            {
                startValue = value;
                nextValue = value;
            }
        }

        public int Step { get; set; }

        public string Format { get; set; }

        public CounterParameter()
        {
            StartValue = 1;
            Step = 1;
        }

        public string GetValue()
        {
            string currentValue = Format != null
                ? nextValue.ToString(Format, CultureInfo.CurrentCulture)
                : nextValue.ToString(CultureInfo.CurrentCulture);

            nextValue += Step;

            return currentValue;
        }
    }
}