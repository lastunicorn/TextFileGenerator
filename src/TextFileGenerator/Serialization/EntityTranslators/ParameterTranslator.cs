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
    }
}