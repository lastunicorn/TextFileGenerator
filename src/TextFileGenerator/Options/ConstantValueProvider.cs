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

        public string MoveToNextValue()
        {
            return value;
        }

        public string CurrentValue
        {
            get { return value; }
        }

        public void Reset()
        {
        }
    }
}