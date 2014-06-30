using System;
using System.Globalization;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public class ParameterTranslator
    {
        public static parameter CreateParameter(Parameter sourceParameter)
        {
            return new parameter
            {
                key = sourceParameter.Key,
                Item = CreateValueProvider(sourceParameter)
            };
        }

        private static object CreateValueProvider(Parameter sourceParameter)
        {
            if (sourceParameter.ValueProvider == null)
                return null;

            Type valueProviderType = sourceParameter.ValueProvider.GetType();

            if (valueProviderType == typeof(ConstantValueProvider))
            {
                ConstantValueProvider sourceValueProvider = (ConstantValueProvider)sourceParameter.ValueProvider;
                return CreateConstantValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(CounterValueProvider))
            {
                CounterValueProvider sourceValueProvider = (CounterValueProvider)sourceParameter.ValueProvider;
                return CreateCounterValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(RandomNumberValueProvider))
            {
                RandomNumberValueProvider sourceValueProvider = (RandomNumberValueProvider)sourceParameter.ValueProvider;
                return CreateRandomNumberValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(RandomTextValueProvider))
            {
                RandomTextValueProvider sourceValueProvider = (RandomTextValueProvider)sourceParameter.ValueProvider;
                return CreateRandomTextValueProvider(sourceValueProvider);
            }

            return null;
        }

        private static parameterRandomText CreateRandomTextValueProvider(RandomTextValueProvider sourceValueProvider)
        {
            return new parameterRandomText
            {
                minLength = sourceValueProvider.MinLength.ToString(CultureInfo.InvariantCulture),
                maxLength = sourceValueProvider.MaxLength.ToString(CultureInfo.InvariantCulture)
            };
        }

        private static parameterRandomNumber CreateRandomNumberValueProvider(RandomNumberValueProvider sourceValueProvider)
        {
            return new parameterRandomNumber
            {
                format = sourceValueProvider.Format,
                minValue = sourceValueProvider.MinValue.ToString(CultureInfo.InvariantCulture),
                maxValue = sourceValueProvider.MaxValue.ToString(CultureInfo.InvariantCulture)
            };
        }

        private static object CreateCounterValueProvider(CounterValueProvider sourceValueProvider)
        {
            return new parameterCounter
            {
                format = sourceValueProvider.Format,
                startValue = sourceValueProvider.StartValue.ToString(CultureInfo.InvariantCulture),
                step = sourceValueProvider.Step.ToString(CultureInfo.InvariantCulture)
            };
        }

        private static object CreateConstantValueProvider(ConstantValueProvider sourceValueProvider)
        {
            return new parameterConstant
            {
                value = sourceValueProvider.Value
            };
        }

        public static Parameter CreateParameter(parameter sourceParameter)
        {
            Parameter destinationParameter = new Parameter();
            destinationParameter.Key = sourceParameter.key;

            Type valueProviderType = sourceParameter.Item.GetType();

            if (valueProviderType == typeof(parameterConstant))
            {
                parameterConstant sourceValueProvider = (parameterConstant)sourceParameter.Item;

                destinationParameter.ValueProvider = CreateConstantValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(parameterCounter))
            {
                parameterCounter sourceValueProvider = (parameterCounter)sourceParameter.Item;

                destinationParameter.ValueProvider = CreateCounterValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(parameterRandomNumber))
            {
                parameterRandomNumber sourceValueProvider = (parameterRandomNumber)sourceParameter.Item;

                destinationParameter.ValueProvider = CreateRandomNumberValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(parameterRandomText))
            {
                parameterRandomText sourceValueProvider = (parameterRandomText)sourceParameter.Item;

                destinationParameter.ValueProvider = CreateRandomTextValueProvider(sourceValueProvider);
            }

            return destinationParameter;
        }

        private static RandomTextValueProvider CreateRandomTextValueProvider(parameterRandomText sourceValueProvider)
        {
            return new RandomTextValueProvider
            {
                MinLength = int.Parse(sourceValueProvider.minLength),
                MaxLength = int.Parse(sourceValueProvider.maxLength)
            };
        }

        private static RandomNumberValueProvider CreateRandomNumberValueProvider(parameterRandomNumber sourceValueProvider)
        {
            return new RandomNumberValueProvider
            {
                Format = sourceValueProvider.format,
                MinValue = int.Parse(sourceValueProvider.minValue),
                MaxValue = int.Parse(sourceValueProvider.maxValue)
            };
        }

        private static CounterValueProvider CreateCounterValueProvider(parameterCounter sourceValueProvider)
        {
            return new CounterValueProvider
            {
                Format = sourceValueProvider.format,
                StartValue = int.Parse(sourceValueProvider.startValue),
                Step = int.Parse(sourceValueProvider.step)
            };
        }

        private static ConstantValueProvider CreateConstantValueProvider(parameterConstant soureceValueProvider)
        {
            return new ConstantValueProvider
            {
                Value = soureceValueProvider.value
            };
        }
    }
}