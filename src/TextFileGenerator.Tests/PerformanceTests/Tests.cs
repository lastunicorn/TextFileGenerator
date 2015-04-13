using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.PerformanceTests
{
    [TestFixture]
    [Ignore]
    public class Tests
    {
        [Test]
        public void MeasureCounterValueProvider()
        {
            MeasureValueProvider(new CounterValueProvider());
        }

        [Test]
        public void MeasureRandomNumberValueProvider()
        {
            MeasureValueProvider(new RandomNumberValueProvider());
        }

        [Test]
        public void MeasureRandomTextValueProvider()
        {
            MeasureValueProvider(new RandomTextValueProvider { MinLength = 10, MaxLength = 10 });
        }

        [Test]
        public void TestAllValueProviders()
        {
            var a = new CounterValueProvider();
            var b = new CounterValueProvider();
            var c = new RandomNumberValueProvider { MinValue = 10, MaxValue = 99 };
            var d = new RandomTextValueProvider { MinLength = 10, MaxLength = 10 };

            TimeSpan timeSpan = Measure(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    a.GetNextValue();
                    b.GetNextValue();
                    c.GetNextValue();
                    d.GetNextValue();
                }
            });

            Trace.WriteLine(timeSpan);
        }

        public string s;

        [Test]
        public void TestDataConcatenation()
        {
            TimeSpan timeSpan = Measure(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("item_");
                    sb.Append("000001");
                    sb.Append("_");
                    sb.Append("000002");
                    sb.Append("_");
                    sb.Append("10");
                    sb.Append("_");
                    sb.Append("fksmvkspec");

                    s = sb.ToString();
                }
            });

            Trace.WriteLine(timeSpan);
        }

        public Parameter parameter;

        [Test]
        public void TestParameterSearch()
        {
            Parameter[] parameters = {
                new Parameter { Name = "r", ValueProvider = new CounterValueProvider() },
                new Parameter { Name = "c", ValueProvider = new CounterValueProvider() },
                new Parameter { Name = "p1", ValueProvider = new RandomNumberValueProvider() },
                new Parameter { Name = "p2", ValueProvider = new RandomTextValueProvider() }
            };

            TimeSpan timeSpan = Measure(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    parameter = parameters.FirstOrDefault(x => x.Name == "p2");
                }
            });

            Trace.WriteLine(timeSpan);
        }

        [Test]
        public void Test()
        {
            //SectionText sectionText = new SectionText { Value = "item_{r}_{c}_{p1}_{p2}" };

            Parameter[] parameters = {
                new Parameter { Name = "r", ValueProvider = new CounterValueProvider() },
                new Parameter { Name = "c", ValueProvider = new CounterValueProvider() },
                new Parameter { Name = "p1", ValueProvider = new RandomNumberValueProvider { MinValue = 10, MaxValue = 99 } },
                new Parameter { Name = "p2", ValueProvider = new RandomTextValueProvider { MinLength = 10, MaxLength = 10 } }
            };

            TimeSpan timeSpan = Measure(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    s = "item_";
                    s += GetParameter(parameters, "r").NextValue;
                    s += "_";
                    s += GetParameter(parameters, "c").NextValue;
                    s += "_";
                    s += GetParameter(parameters, "p1").NextValue;
                    s += "_";
                    s += GetParameter(parameters, "p2").NextValue;

                    //s = "item_" +
                    //    GetParameter(parameters, "r").NextValue + "_" +
                    //    GetParameter(parameters, "c").NextValue + "_" +
                    //    GetParameter(parameters, "p1").NextValue + "_" +
                    //    GetParameter(parameters, "p2").NextValue;

                    //StringBuilder sb = new StringBuilder();

                    //sb.Append("item_");
                    //sb.Append(GetParameter(parameters, "r").NextValue);
                    //sb.Append("_");
                    //sb.Append(GetParameter(parameters, "c").NextValue);
                    //sb.Append("_");
                    //sb.Append(GetParameter(parameters, "p1").NextValue);
                    //sb.Append("_");
                    //sb.Append(GetParameter(parameters, "p2").NextValue);

                    //s = sb.ToString();
                }
            });

            Trace.WriteLine(timeSpan);
        }

        private static Parameter GetParameter(IEnumerable<Parameter> parameters, string parameterName)
        {
            return parameters.FirstOrDefault(x => x.Name == parameterName);
        }

        private static void MeasureValueProvider(IValueProvider valueProvider)
        {
            TimeSpan timeSpan = Measure(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    valueProvider.GetNextValue();
                }
            });

            Trace.WriteLine(timeSpan);
        }

        private static TimeSpan Measure(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }
    }
}
