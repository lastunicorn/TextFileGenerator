using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class ConstantParameter : IParameter
    {
        private string value;

        public string Key { get; set; }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public string GetValue()
        {
            return value;
        }
    }
}