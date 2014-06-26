using System;
using System.Reflection;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class CustomParameter : IParameter
    {
        private readonly MethodInfo methodInfo;
        private readonly object obj;

        public string Key { get; set; }

        public CustomParameter(string classType, string methodName)
        {
            Type type = Type.GetType(classType);

            if (type == null)
                throw new Exception(string.Format("Type '{0}' could not be found.", classType));

            methodInfo = type.GetMethod(methodName);

            if (methodInfo == null)
                throw new Exception(string.Format("Method '{0}' does not exist in type '{1}'.", methodName, classType));

            if (methodInfo.IsStatic)
            {
                obj = null;
            }
            else
            {
                ConstructorInfo constructorInfo = type.GetConstructor(null);
                obj = constructorInfo.Invoke(null);
            }

        }

        public string GetValue()
        {
            return methodInfo.Invoke(obj, null).ToString();
        }
    }
}
