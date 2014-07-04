using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public class SectionTranslator
    {
        public static section Translate(Section sourceSection)
        {
            section destinationSection = new section
            {
                name = sourceSection.Name,
                repeat = sourceSection.RepeatCount.ToString(CultureInfo.InvariantCulture),
                separator = sourceSection.Separator,
                separatorType = Translate(sourceSection.SeparatorType)
            };

            if (sourceSection.Template != null)
            {
                destinationSection.Items = new object[]
                {
                    sourceSection.Template.Value
                };
            }
            else if (sourceSection.Sections.Count > 0)
            {
                destinationSection.Items = new object[sourceSection.Sections.Count];

                for (int i = 0; i < sourceSection.Sections.Count; i++)
                {
                    destinationSection.Items[i] = Translate(sourceSection.Sections[i]);
                }
            }

            CreateParameters(destinationSection, sourceSection.Parameters);

            return destinationSection;
        }

        private static separatorType Translate(SeparatorType value)
        {
            switch (value)
            {
                default:
                case SeparatorType.Infix:
                    return separatorType.Infix;

                case SeparatorType.Prefix:
                    return separatorType.Prefix;

                case SeparatorType.Postfix:
                    return separatorType.Postfix;
            }
        }

        private static void CreateParameters(section destinationSection, IList<Parameter> sourceParameters)
        {
            if (sourceParameters.Count == 0)
                return;

            destinationSection.parameter = new parameter[sourceParameters.Count];

            for (int i = 0; i < sourceParameters.Count; i++)
            {
                Parameter sourceParameter = sourceParameters[i];
                parameter destinationParameter = ParameterTranslator.CreateParameter(sourceParameter);

                destinationSection.parameter[i] = destinationParameter;
            }
        }

        public static Section Translate(section sourceSection)
        {
            Section destinationSection = new Section
            {
                Name = sourceSection.name,
                RepeatCount = int.Parse(sourceSection.repeat),
                Separator = sourceSection.separator,
                SeparatorType = Translate(sourceSection.separatorType),
            };

            if (sourceSection.Items != null && sourceSection.Items.Length > 0)
            {
                foreach (object item in sourceSection.Items)
                {
                    if (item == null)
                        continue;

                    Type itemType = item.GetType();

                    if (itemType == typeof(string))
                        destinationSection.Template = new Template
                        {
                            Value = (string)item
                        };

                    if (itemType == typeof(section))
                    {
                        Section destinationSubsection = Translate((section)item);
                        destinationSection.Sections.Add(destinationSubsection);
                    }
                }
            }

            CreateParameters(destinationSection, sourceSection.parameter);

            return destinationSection;
        }

        private static void CreateParameters(Section destinationSection, IEnumerable<parameter> sourceParameters)
        {
            if (sourceParameters == null)
                return;

            IEnumerable<Parameter> destinationParameters = sourceParameters.Select(ParameterTranslator.CreateParameter);
            destinationSection.Parameters.AddRange(destinationParameters);
        }

        private static SeparatorType Translate(separatorType sourceSeparatorType)
        {
            switch (sourceSeparatorType)
            {
                case separatorType.Infix:
                    return SeparatorType.Infix;

                case separatorType.Prefix:
                    return SeparatorType.Prefix;

                case separatorType.Postfix:
                    return SeparatorType.Postfix;

                default:
                    return SeparatorType.Infix;
            }
        }
    }
}