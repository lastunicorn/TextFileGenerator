using System.Globalization;

namespace DustInTheWind.TextFileGenerator.Options
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