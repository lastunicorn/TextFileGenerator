namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class EmptyValueProvider : IValueProvider
    {
        public string GetValue()
        {
            return string.Empty;
        }
    }
}