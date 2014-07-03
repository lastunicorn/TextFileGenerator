namespace DustInTheWind.TextFileGenerator.Options
{
    public interface IValueProvider
    {
        string GetNextValue();
        string GetCurrentValue();
        void Reset();
    }
}