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
                WriteSection(textWriter, section, null);
            }

            textWriter.Flush();
        }

        private static void WriteSection(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            bool existsSeparator = !string.IsNullOrEmpty(section.Separator);

            foreach (Parameter parameter in section.Parameters)
            {
                parameter.Reset();
            }

            for (int i = 0; i < section.RepeatCount; i++)
            {
                if (existsSeparator)
                    WriteSeparatorBeforeItem(textWriter, section.Separator, section.SeparatorType, i);

                WriteSectionContent(textWriter, section, additionalParameters);

                if (existsSeparator)
                    WriteSeparatorAfterItem(textWriter, section.Separator, section.SeparatorType);
            }
        }

        private static void WriteSectionContent(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            if (section.Template != null)
            {
                WriteSectionTemplate(textWriter, section, additionalParameters);
            }
            else
            {
                foreach (Parameter parameter in section.Parameters)
                {
                    parameter.GetNextValue();
                }

                foreach (Section subSection in section.Sections)
                {
                    WriteSection(textWriter, subSection, section.Parameters);
                }
            }
        }

        private static void WriteSectionTemplate(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            string text = section.Template;

            foreach (Parameter parameter in section.Parameters)
            {
                string key = "{" + parameter.Key + "}";
                string value = parameter.GetNextValue();

                text = text.Replace(key, value);
            }

            if (additionalParameters != null)
                foreach (Parameter parameter in additionalParameters)
                {
                    string key = "{" + parameter.Key + "}";
                    string value = parameter.GetCurrentValue();

                    text = text.Replace(key, value);
                }

            textWriter.Write(text);
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