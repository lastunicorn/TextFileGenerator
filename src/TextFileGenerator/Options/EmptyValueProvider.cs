namespace DustInTheWind.TextFileGenerator.Options
{
    public class EmptyValueProvider : IValueProvider
    {
        public string GetValue()
        {
            return string.Empty;
        }
    }
}