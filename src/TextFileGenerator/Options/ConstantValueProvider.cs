namespace DustInTheWind.TextFileGenerator.Options
{
    public class ConstantValueProvider : IValueProvider
    {
        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public string GetNextValue()
        {
            return value;
        }

        public string GetCurrentValue()
        {
            return value;
        }

        public void Reset()
        {
        }
    }
}