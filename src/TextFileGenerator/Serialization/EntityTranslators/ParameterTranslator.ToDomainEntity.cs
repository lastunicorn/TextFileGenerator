using System;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public static partial class ParameterTranslator
    {
        public static Parameter ToDomainEntity(parameter sourceParameter)
        {
            return new Parameter
            {
                Name = sourceParameter.name,
                ValueProvider = CreateValueProvider(sourceParameter)
            };
        }

        private static IValueProvider CreateValueProvider(parameter sourceParameter)
        {
            Type valueProviderType = sourceParameter.Item.GetType();

            if (valueProviderType == typeof(parameterConstant))
            {
                parameterConstant sourceValueProvider = (parameterConstant)sourceParameter.Item;
                return CreateConstantValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(parameterCounter))
            {
                parameterCounter sourceValueProvider = (parameterCounter)sourceParameter.Item;
                return CreateCounterValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(parameterRandomNumber))
            {
                parameterRandomNumber sourceValueProvider = (parameterRandomNumber)sourceParameter.Item;
                return CreateRandomNumberValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(parameterRandomText))
            {
                parameterRandomText sourceValueProvider = (parameterRandomText)sourceParameter.Item;
                return CreateRandomTextValueProvider(sourceValueProvider);
            }

            return null;
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