namespace DustInTheWind.TextFileGenerator.Options
{
    public interface IParameter
    {
        string Key { get; }
        string GetValue();
    }
}