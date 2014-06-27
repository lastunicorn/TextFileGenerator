using System;
using System.Globalization;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public class EntityCreator
    {
        private readonly GeneratorOptions sourceOptions;
        private textFileGenerator destinationOptions;

        public EntityCreator(GeneratorOptions sourceOptions)
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
                section destinationSection = CreateSection(sourceSection);

                destinationOptions.sections[i] = destinationSection;
            }
        }

        private static section CreateSection(Section sourceSection)
        {
            section destinationSection = new section();

            destinationSection.name = sourceSection.Name;
            destinationSection.repeat = sourceSection.RepeatCount.ToString(CultureInfo.InvariantCulture);
            destinationSection.separator = sourceSection.Separator;

            switch (sourceSection.SeparatorType)
            {
                case SeparatorType.Infix:
                    destinationSection.separatorType = separatorType.Infix;
                    break;

                case SeparatorType.Prefix:
                    destinationSection.separatorType = separatorType.Prefix;
                    break;

                case SeparatorType.Postfix:
                    destinationSection.separatorType = separatorType.Postfix;
                    break;
            }

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
                    destinationSection.Items[i] = CreateSection(sourceSection.Sections[i]);
                }
            }

            bool existsParameters = sourceSection.Parameters.Count > 0;

            if (existsParameters)
                CreateParameters(sourceSection, destinationSection);

            return destinationSection;
        }

        private static void CreateParameters(Section sourceSection, section destinationSection)
        {
            destinationSection.parameter = new parameter[sourceSection.Parameters.Count];

            for (int i = 0; i < sourceSection.Parameters.Count; i++)
            {
                Parameter sourceParameter = sourceSection.Parameters[i];
                parameter destinationParameter = CreateParameter(sourceParameter);

                destinationSection.parameter[i] = destinationParameter;
            }
        }

        private static parameter CreateParameter(Parameter sourceParameter)
        {
            parameter destinationParameter = new parameter();
            destinationParameter.key = sourceParameter.Key;
            destinationParameter.Item = CreateValueProvider(sourceParameter);

            return destinationParameter;
        }

        private static object CreateValueProvider(Parameter sourceParameter)
        {
            if (sourceParameter.ValueProvider == null)
                return null;

            Type valueProviderType = sourceParameter.ValueProvider.GetType();

            if (valueProviderType == typeof(ConstantValueProvider))
            {
                ConstantValueProvider sourceValueProvider = (ConstantValueProvider)sourceParameter.ValueProvider;

                parameterConstant parameterConstant = new parameterConstant();
                parameterConstant.value = sourceValueProvider.Value;

                return parameterConstant;
            }

            if (valueProviderType == typeof(CounterValueProvider))
            {
                CounterValueProvider sourceValueProvider = (CounterValueProvider)sourceParameter.ValueProvider;

                parameterCounter parameterCounter = new parameterCounter();
                parameterCounter.format = sourceValueProvider.Format;
                parameterCounter.startValue = sourceValueProvider.StartValue.ToString(CultureInfo.InvariantCulture);
                parameterCounter.step = sourceValueProvider.Step.ToString(CultureInfo.InvariantCulture);

                return parameterCounter;
            }

            if (valueProviderType == typeof(RandomNumberValueProvider))
                return new parameterRandomNumber();

            if (valueProviderType == typeof(RandomTextValueProvider))
                return new parameterRandomText();

            return null;
        }
    }
}
