using System.Collections.Generic;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class Template
    {
        public string Value { get; set; }

        public string Format(IEnumerable<Parameter> parameters, IEnumerable<Parameter> additionalParameters)
        {
            if (Value == null)
                return string.Empty;

            string text = Value;

            text = FormatUsingParameters(text, parameters);
            text = FormatUsingAdditionalParameters(text, additionalParameters);

            return text;
        }

        private static string FormatUsingParameters(string text, IEnumerable<Parameter> parameters)
        {
            if (parameters == null)
                return text;

            foreach (Parameter parameter in parameters)
            {
                string key = FormatParameterKey(parameter);
                string value = parameter.NextValue;

                text = text.Replace(key, value);
            }

            return text;
        }

        private static string FormatUsingAdditionalParameters(string text, IEnumerable<Parameter> additionalParameters)
        {
            if (additionalParameters == null)
                return text;

            foreach (Parameter parameter in additionalParameters)
            {
                string key = FormatParameterKey(parameter);
                string value = parameter.CurrentValue;

                text = text.Replace(key, value);
            }

            return text;
        }

        private static string FormatParameterKey(Parameter parameter)
        {
            return "{" + parameter.Key + "}";
        }
    }
}
