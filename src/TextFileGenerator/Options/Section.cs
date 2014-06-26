using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Section
    {
        public string Name { get; set; }
        public string Template { get; set; }
        public int Count { get; set; }
        public string Separator { get; set; }
        public SeparatorType SeparatorType { get; set; }
        public List<IParameter> Parameters { get; set; }

        public Section()
        {
            Count = 1;
            Parameters = new List<IParameter>();
        }
    }
}