// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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