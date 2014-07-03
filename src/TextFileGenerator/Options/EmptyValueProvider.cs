namespace DustInTheWind.TextFileGenerator.Options
{
    public class EmptyValueProvider : IValueProvider
    {
        public string GetNextValue()
        {
            return string.Empty;
        }

        public string GetCurrentValue()
        {
            return string.Empty;
        }

        public void Reset()
        {
        }
    }
}