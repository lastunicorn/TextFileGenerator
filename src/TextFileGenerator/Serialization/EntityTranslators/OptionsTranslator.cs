using System.Collections.Generic;
using System.Linq;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public class OptionsTranslator
    {
        public static textFileGenerator Translate(GeneratorOptions sourceOptions)
        {
            textFileGenerator destinationOptions = new textFileGenerator();

            CreateSections(destinationOptions, sourceOptions.Sections);

            return destinationOptions;
        }

        private static void CreateSections(textFileGenerator destinationOptions, IReadOnlyList<Section> sourceSections)
        {
            if (sourceSections == null || sourceSections.Count == 0)
                return;

            destinationOptions.sections = new section[sourceSections.Count];

            for (int i = 0; i < sourceSections.Count; i++)
            {
                section destinationSection = SectionTranslator.Translate(sourceSections[i]);
                destinationOptions.sections[i] = destinationSection;
            }
        }

        public static GeneratorOptions Translate(textFileGenerator sourceOptions)
        {
            GeneratorOptions destinationOptions = new GeneratorOptions();

            CreateSections(destinationOptions, sourceOptions.sections);

            return destinationOptions;
        }

        private static void CreateSections(GeneratorOptions destinationOptions, IEnumerable<section> sourceSections)
        {
            if (sourceSections == null)
                return;

            IEnumerable<Section> destinationSections = sourceSections.Select(SectionTranslator.Translate);
            destinationOptions.Sections.AddRange(destinationSections);
        }
    }
}
