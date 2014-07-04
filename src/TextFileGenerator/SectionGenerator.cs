using System.Collections.Generic;
using System.IO;
using System.Linq;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator
{
    public class SectionGenerator
    {
        public static void WriteSection(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
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
                WriteSectionTemplate(textWriter, section, additionalParameters);
            else
                WriteSubsections(textWriter, section, additionalParameters);
        }

        private static void WriteSectionTemplate(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            string text = section.Template.Format(section.Parameters, additionalParameters);
            textWriter.Write(text);
        }

        private static void WriteSubsections(TextWriter textWriter, Section section, IEnumerable<Parameter> additionalParameters)
        {
            section.Parameters.MoveAllToNextValue();

            IEnumerable<Parameter> calculatedAdditionalParameters = ConcatenateAdditionalParameters(section.Parameters, additionalParameters);

            foreach (Section subSection in section.Sections)
            {
                WriteSection(textWriter, subSection, calculatedAdditionalParameters);
            }
        }

        private static IEnumerable<Parameter> ConcatenateAdditionalParameters(IEnumerable<Parameter> parameters, IEnumerable<Parameter> additionalParameters)
        {
            if (parameters == null)
                return additionalParameters;

            if (additionalParameters == null)
                return parameters;

            return parameters.Concat(additionalParameters);
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