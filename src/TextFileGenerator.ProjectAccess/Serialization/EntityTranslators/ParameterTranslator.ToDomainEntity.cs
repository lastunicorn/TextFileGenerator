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
        public static Parameter ToDomainEntity(this XParameter sourceParameter)
        {
            return new Parameter
            {
                Name = sourceParameter.Name,
                ValueProvider = CreateValueProvider(sourceParameter),
                ValueChangeMode = CalculateValuePersistence(sourceParameter)
            };
        }

        private static ValueChangeMode CalculateValuePersistence(XParameter sourceParameter)
        {
            switch (sourceParameter.ValuePersistence)
            {
                case XParameterValuePersistence.PerRequest:
                    return ValueChangeMode.Auto;

                case XParameterValuePersistence.PerSectionStep:
                    return ValueChangeMode.Manual;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static IValueProvider CreateValueProvider(XParameter sourceParameter)
        {
            Type valueProviderType = sourceParameter.Item.GetType();

            if (valueProviderType == typeof(XParameterConstant))
            {
                XParameterConstant sourceValueProvider = (XParameterConstant)sourceParameter.Item;
                return CreateConstantValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(XParameterCounter))
            {
                XParameterCounter sourceValueProvider = (XParameterCounter)sourceParameter.Item;
                return CreateCounterValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(XParameterRandomNumber))
            {
                XParameterRandomNumber sourceValueProvider = (XParameterRandomNumber)sourceParameter.Item;
                return CreateRandomNumberValueProvider(sourceValueProvider);
            }

            if (valueProviderType == typeof(XParameterRandomText))
            {
                XParameterRandomText sourceValueProvider = (XParameterRandomText)sourceParameter.Item;
                return CreateRandomTextValueProvider(sourceValueProvider);
            }

            return null;
        }

        private static RandomTextValueProvider CreateRandomTextValueProvider(XParameterRandomText sourceValueProvider)
        {
            return new RandomTextValueProvider
            {
                MinLength = int.Parse(sourceValueProvider.MinLength),
                MaxLength = int.Parse(sourceValueProvider.MaxLength)
            };
        }

        private static RandomNumberValueProvider CreateRandomNumberValueProvider(XParameterRandomNumber sourceValueProvider)
        {
            return new RandomNumberValueProvider
            {
                Format = sourceValueProvider.Format,
                MinValue = int.Parse(sourceValueProvider.MinValue),
                MaxValue = int.Parse(sourceValueProvider.MaxValue)
            };
        }

        private static CounterValueProvider CreateCounterValueProvider(XParameterCounter sourceValueProvider)
        {
            return new CounterValueProvider
            {
                Format = sourceValueProvider.Format,
                StartValue = int.Parse(sourceValueProvider.StartValue),
                Step = int.Parse(sourceValueProvider.Step)
            };
        }

        private static ConstantValueProvider CreateConstantValueProvider(XParameterConstant sourceValueProvider)
        {
            return new ConstantValueProvider
            {
                Value = sourceValueProvider.Value
            };
        }
    }
}