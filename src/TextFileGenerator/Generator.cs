using System;
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

        public void Generate(TextWriter textWriter)
        {
            for (int i = 0; i < options.Sections.Count; i++)
            {
                WriteSection(textWriter, options.Sections[i]);
            }

            textWriter.Flush();
        }

        private static void WriteSection(TextWriter textWriter, Section section)
        {
            bool existsSeparator = !string.IsNullOrEmpty(section.Separator);

            for (int i = 0; i < section.RepeatCount; i++)
            {
                if (existsSeparator)
                    WriteSeparatorBeforeItem(textWriter, section.Separator, section.SeparatorType, i);

                string text = section.Template;

                foreach (Parameter parameter in section.Parameters)
                {
                    string key = "{" + parameter.Key + "}";
                    string value = parameter.GetValue();

                    text = text.Replace(key, value);
                }

                textWriter.Write(text);

                if (existsSeparator)
                    WriteSeparatorAfterItem(textWriter, section.Separator, section.SeparatorType);
            }
        }

        private static void WriteSeparatorAfterItem(TextWriter textWriter, string separator, SeparatorType separatorType)
        {
            bool shouldWriteSeparatorAfterSection = separatorType == SeparatorType.Postfix;

            if (shouldWriteSeparatorAfterSection)
                textWriter.Write(separator);
        }

        private static void WriteSeparatorBeforeItem(TextWriter textWriter, string separator, SeparatorType separatorType, int i)
        {
            bool shouldWriteSeparatorBeforeSection = separatorType == SeparatorType.Prefix ||
                                                      (separatorType == SeparatorType.Infix && i > 0);

            if (shouldWriteSeparatorBeforeSection)
                textWriter.Write(separator);
        }
    }
}