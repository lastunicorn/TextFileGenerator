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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.EntityTranslators
{
    public static partial class ParameterTranslator
    {
        public static Parameter ToDomainEntity(parameter sourceParameter)
        {
            return new Parameter
            {
                Name = sourceParameter.name,
                ValueProvider = CreateValueProvider(sourceParameter),
                ValueChangeMode = CalculateValuePersistence(sourceParameter)
            };
        }

        private static ValueChangeMode CalculateValuePersistence(parameter sourceParameter)
        {
            switch (sourceParameter.valuePersistence)
            {
                case parameterValuePersistence.PerRequest:
                    return ValueChangeMode.Auto;

                case parameterValuePersistence.PerSectionStep:
                    return ValueChangeMode.Manual;

                default:
                    throw new ArgumentOutOfRangeException();
            }
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

        private static ConstantValueProvider CreateConstantValueProvider(parameterConstant sourceValueProvider)
        {
            return new ConstantValueProvider
            {
                Value = sourceValueProvider.value
            };
        }
    }
}