using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class SectionList : List<Section>
    {
        public string Separator { get; set; }
        public SeparatorType SeparatorType { get; set; }
    }
}
