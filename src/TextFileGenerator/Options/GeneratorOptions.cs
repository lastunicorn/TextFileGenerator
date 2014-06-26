namespace DustInTheWind.TextFileGenerator.Options
{
    public class GeneratorOptions
    {
        public SectionList Sections { get; private set; }

        public GeneratorOptions()
        {
            Sections = new SectionList();
        }
    }
}