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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using XParameter = DustInTheWind.TextFileGenerator.Serialization.Parameter;
using XParameterValuePersistence = DustInTheWind.TextFileGenerator.Serialization.ParameterValuePersistence;
using XParameterRandomText = DustInTheWind.TextFileGenerator.Serialization.ParameterRandomText;
using XParameterRandomNumber = DustInTheWind.TextFileGenerator.Serialization.ParameterRandomNumber;
using XParameterCounter = DustInTheWind.TextFileGenerator.Serialization.ParameterCounter;
using XParameterConstant = DustInTheWind.TextFileGenerator.Serialization.ParameterConstant;

namespace DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.EntityTranslators
{
    public static partial class ParameterTranslator
    {
        public static XParameter ToXmlEntity(this Parameter sourceParameter)
        {
            return new XParameter
            {
                Name = sourceParameter.Name,
                Item = CreateValueProvider(sourceParameter),
                ValuePersistence = CalculateValuePersistence(sourceParameter)
            };
        }

        private static XParameterValuePersistence CalculateValuePersistence(Parameter sourceParameter)
        {
            switch (sourceParameter.ValueChangeMode)
            {
                case ValueChangeMode.Auto:
                    return XParameterValuePersistence.PerRequest;

                case ValueChangeMode.Manual:
                    return XParameterValuePersistence.PerSectionStep;

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

        private static XParameterRandomText CreateRandomTextValueProvider(RandomTextValueProvider sourceValueProvider)
        {
            return new XParameterRandomText
            {
                MinLength = sourceValueProvider.MinLength.ToString(CultureInfo.InvariantCulture),
                MaxLength = sourceValueProvider.MaxLength.ToString(CultureInfo.InvariantCulture)
            };
        }

        private static XParameterRandomNumber CreateRandomNumberValueProvider(RandomNumberValueProvider sourceValueProvider)
        {
            return new XParameterRandomNumber
            {
                Format = sourceValueProvider.Format,
                MinValue = sourceValueProvider.MinValue.ToString(CultureInfo.InvariantCulture),
                MaxValue = sourceValueProvider.MaxValue.ToString(CultureInfo.InvariantCulture)
            };
        }

        private static XParameterCounter CreateCounterValueProvider(CounterValueProvider sourceValueProvider)
        {
            return new XParameterCounter
            {
                Format = sourceValueProvider.Format,
                StartValue = sourceValueProvider.StartValue.ToString(CultureInfo.InvariantCulture),
                Step = sourceValueProvider.Step.ToString(CultureInfo.InvariantCulture)
            };
        }

        private static XParameterConstant CreateConstantValueProvider(ConstantValueProvider sourceValueProvider)
        {
            return new XParameterConstant
            {
                Value = sourceValueProvider.Value
            };
        }
    }
}