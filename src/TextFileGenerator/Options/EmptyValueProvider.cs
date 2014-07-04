namespace DustInTheWind.TextFileGenerator.Options
{
    public class EmptyValueProvider : IValueProvider
    {
        public string MoveToNextValue()
        {
            return string.Empty;
        }

        public string CurrentValue
        {
            get { return string.Empty; }
        }

        public void Reset()
        {
        }
    }
}