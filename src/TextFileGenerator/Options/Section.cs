using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Section
    {
        public string Name { get; set; }
        public Template Template { get; set; }
        public int RepeatCount { get; set; }
        public string Separator { get; set; }
        public SeparatorType SeparatorType { get; set; }
        public SectionParameterList Parameters { get; private set; }
        public List<Section> Sections { get; private set; }

        public Section()
        {
            RepeatCount = 1;
            Parameters = new SectionParameterList();
            Sections = new List<Section>();
        }
    }
}