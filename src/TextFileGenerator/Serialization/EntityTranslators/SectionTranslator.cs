using System.Collections.Generic;
using System.Globalization;
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
                    sourceSection.Template
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

            CreateAndAddParameters(destinationSection, sourceSection.Parameters);

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

        private static void CreateAndAddParameters(section destinationSection, IList<Parameter> sourceParameters)
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
    }
}