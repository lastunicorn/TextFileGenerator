namespace DustInTheWind.TextFileGenerator.Options
{
    public interface IParameter
    {
        string Key { get; set; }
        string GetValue();
    }
}