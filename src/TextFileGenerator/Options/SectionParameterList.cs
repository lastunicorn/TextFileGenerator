using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class SectionParameterList : List<Parameter>
    {
        public void MoveAllToNextValue()
        {
            foreach (Parameter parameter in this)
            {
                parameter.MoveToNextValue();
            }
        }
    }
}
