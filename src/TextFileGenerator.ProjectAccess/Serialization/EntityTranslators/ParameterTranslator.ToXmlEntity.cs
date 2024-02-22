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
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.ValueProviders;

namespace DustInTheWind.TextFileGenerator.Serialization.EntityTranslators
{
    public static partial class ParameterTranslator
    {
        public static parameter ToXmlEntity(Parameter sourceParameter)
        {
            return new parameter
            {
                name = sourceParameter.Name,
                Item = CreateValueProvider(sourceParameter),
                valuePersistence = CalculateValuePersistence(sourceParameter)
            };
        }

        private static parameterValuePersistence CalculateValuePersistence(Parameter sourceParameter)
        {
            switch (sourceParameter.ValueChangeMode)
            {
                case ValueChangeMode.Auto:
                    return parameterValuePersistence.PerRequest;

                case ValueChangeMode.Manual:
                    return parameterValuePersistence.PerSectionStep;

                default:
                    throw new ArgumentOutOfRangeException();
            }
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