using System;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public class GeneratorOptionsTranslator
    {
        private readonly GeneratorOptions sourceOptions;
        private textFileGenerator destinationOptions;

        public GeneratorOptionsTranslator(GeneratorOptions sourceOptions)
        {
            if (sourceOptions == null)
                throw new ArgumentNullException("sourceOptions");

            this.sourceOptions = sourceOptions;
        }

        public textFileGenerator Create()
        {
            destinationOptions = new textFileGenerator();

            bool existsSections = sourceOptions.Sections.Count > 0;

            if (existsSections)
                CreateSections();

            return destinationOptions;
        }

        private void CreateSections()
        {
            destinationOptions.sections = new section[sourceOptions.Sections.Count];

            for (int i = 0; i < sourceOptions.Sections.Count; i++)
            {
                Section sourceSection = sourceOptions.Sections[i];
                section destinationSection = SectionTranslator.Translate(sourceSection);

                destinationOptions.sections[i] = destinationSection;
            }
        }
    }
}
