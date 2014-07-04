using System;
using System.Collections.Generic;
using System.IO;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator
{
    public class Generator
    {
        private readonly GeneratorOptions options;

        public Generator(GeneratorOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            this.options = options;
        }

        public void Generate(Stream outputStream)
        {
            TextWriter textWriter = new StreamWriter(outputStream);
            Generate(textWriter);
        }

        public void Generate(TextWriter textWriter)
        {
            foreach (Section section in options.Sections)
            {
                SectionGenerator.WriteSection(textWriter, section, null);
            }

            textWriter.Flush();
        }
    }
}