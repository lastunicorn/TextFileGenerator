namespace DustInTheWind.TextFileGenerator.Options
{
    public interface IValueProvider
    {
        string MoveToNextValue();
        string CurrentValue { get; }
        void Reset();
    }
}