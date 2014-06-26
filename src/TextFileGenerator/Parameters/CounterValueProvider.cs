using System.Globalization;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class CounterValueProvider : IValueProvider
    {
        private int nextValue;
        private int startValue;

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

        public CounterValueProvider()
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