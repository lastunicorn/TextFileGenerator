using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Section
    {
        public string Name { get; set; }
        public string Template { get; set; }
        public int RepeatCount { get; set; }
        public string Separator { get; set; }
        public SeparatorType SeparatorType { get; set; }
        public List<Parameter> Parameters { get; private set; }
        public List<Section> Sections { get; private set; }

        public Section()
        {
            RepeatCount = 1;
            Parameters = new List<Parameter>();
            Sections = new List<Section>();
        }
    }
}